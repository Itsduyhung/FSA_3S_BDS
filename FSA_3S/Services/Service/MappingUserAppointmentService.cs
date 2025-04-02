using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using FSA_3S.Enum;
using FSA_3S.Models.Requests;
using System.Xml;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.SignalR;
using FSA_3S.Repositories.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models;
namespace FSA_3S.Services.Service
{
    public class MappingUserAppointmentService(IMappingUserAppointmentRepository _repository, IHttpContextAccessor _httpContextAccessor, IAppointmentService _appointmentService, INotificationService _notificationService, IHubContext<NotificationHub> _hubContext, IStaffRepository _staffRepository, IUserRepository _userRepository, AppDbContext _context) : IMappingUserAppointmentService
    {
        private async Task SendNotificationToUser(int senderId, int receiverId, string title, string message)
        {
            await _notificationService.NotificationPostRealAsync(senderId, receiverId, title, message);
            if (_hubContext != null)
            {
                await _hubContext.Clients.User(receiverId.ToString()).SendAsync("ReceiveNotification", title, message);
            }
        }

        public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync()
        {
            var mappings = await _repository.GetAllAsync();

            var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.Approve)
                    .Select(m => new MappingUserAppointmentResponse
                    {
                        MappingUserAppointmentId = m.MappingUserAppointmentId,
                        UserId = m.UserId,
                        FullName = m.User != null ? m.User.FullName : "N/A", // Kiểm tra null
                        AppointmentId = m.AppointmentId,
                        AppointmentTitle = m.Appointment != null ? m.Appointment.Title : "N/A", // Kiểm tra null
                        AppointmentDate = m.Appointment.AppointmentDate,
                        CustomerName = m.Appointment != null && m.Appointment.Customer != null
                    ? m.Appointment.Customer.FullName
                    : "N/A"
                    });

            return response.ToList();
        }

        public async Task<MappingUserAppointmentEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<MappingUserAppointmentResponse> CreateAsync(MappingUserAppointmentRequest request)

        {
            int createBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");
            Console.WriteLine($" UserId Retrieved: {createBy}");

            var newAppointment = new AppointmentEntity
            {
                CustomerId = request.CustomerId,
                Title = request.Title ?? "Default Title",
                Description = request.Description,
                AppointmentDate = request.AppointmentDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
                Status = request.Status ?? "Pending",
                Address = request.Address,
                CreatedBy = createBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };
            var createdAppointment = await _appointmentService.CreateAsync(newAppointment);
            var mapping = new MappingUserAppointmentEntity
            {
                UserId = request.UserId,
                AppointmentId = createdAppointment.AppointmentId,
                ApprovalStatus = ApprovalStatusEnum.Waiting_for_approval,
                CreatedBy = createBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            try
            {
                var result = await _repository.AddAsync(mapping);
                // Lấy thông tin người dùng từ CreatedBy
                var createdByUser = await _staffRepository.GetUserByIdAsync(createBy) ?? throw new ArgumentException("Người dùng không tồn tại.");
                string createdByFullName = createdByUser?.FullName ?? "Người dùng không xác định";
                string userRole = createdByUser?.Role ?? "Unknown";

                // Kiểm tra xem người tạo có phải là admin không
                if (userRole != "admin")
                {
                    // Gửi notification cho admin khi staff tạo bất động sản mới
                    var adminId = await _userRepository.GetAdminIdAsync();

                    var message = $"Người dùng {createdByFullName} đã tạo 1 lịch hẹn mới có tiêu đề {request.Title} cần duyệt.";

                    await _notificationService.NotificationPostRealAsync(
                        senderId: createBy,
                        receiverId: adminId,
                        title: "Lịch hẹn mới cần duyệt",
                        message: message
                    );

                    // Gửi thông báo realtime qua SignalR
                    if (_hubContext != null)
                    {
                        await _hubContext.Clients.User(adminId.ToString()).SendAsync("ReceiveNotification", "Lịch hẹn mới cần duyệt.", message);
                    }
                }
                return new MappingUserAppointmentResponse
                {
                    MappingUserAppointmentId = result.MappingUserAppointmentId,
                    UserId = result.UserId,
                    AppointmentId = result.AppointmentId,
                    ApprovalStatus = result.ApprovalStatus,
                    CreatedBy = result.CreatedBy,
                    UpdatedBy = result.UpdatedBy,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt

                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[CreateAsync] Failed to create real estate: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        public async Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping)
        {
            return await _repository.UpdateAsync(mapping);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<MappingUserAppointmentResponse?> UpdateApprovalStatusAsync(int id, ApprovalStatusRequest request)
        {   
            var map = await _repository.GetByIdForPutAsync(id);
            if (map == null) return null;
             var  AppointmentTitle = await _context.Appointments
                .Where(a => a.AppointmentId == map.AppointmentId)
                .Select(a => a.Title)
                .FirstOrDefaultAsync();
            // Lưu trạng thái cũ trước khi cập nhật
            var oldStatus = map.ApprovalStatus;

            // Cập nhật trạng thái duyệt
            map.ApprovalStatus = (ApprovalStatusEnum)request.ApprovalStatus;
            map.UpdatedAt = DateTime.UtcNow;
            
            var response = new MappingUserAppointmentResponse
            {
                ApprovalStatus = map.ApprovalStatus,
                MappingUserAppointmentId = map.MappingUserAppointmentId,
                UserId = map.UserId,

                //Title = map.Title ?? "Default Title",
                AppointmentId = map.AppointmentId,
                CreatedBy = map.CreatedBy,
                UpdatedBy = map.UpdatedBy,
                CreatedAt = map.CreatedAt,
                UpdatedAt = map.UpdatedAt
            };

            var adminId = await _userRepository.GetAdminIdAsync();
            
            if (oldStatus == ApprovalStatusEnum.Waiting_for_approval &&
                (map.ApprovalStatus == ApprovalStatusEnum.Approve || map.ApprovalStatus == ApprovalStatusEnum.Reject))
            {
                var message = $"Admin đã {map.ApprovalStatus.ToString().ToLower()} cuộc hẹn: {AppointmentTitle}.";

                // Gửi thông báo đến người tạo cuộc hẹn
                await _notificationService.NotificationPutRealAsync(
                    senderId: adminId,
                    receiverId: map.CreatedBy,
                    title: "Trạng thái phê duyệt cuộc hẹn",
                    message: message
                );

                // Gửi thông báo realtime cho người tạo cuộc hẹn
                await _hubContext.Clients.User(map.CreatedBy.ToString())
                    .SendAsync("ReceiveNotification", "Trạng thái phê duyệt", message);
            }
            await _repository.UpdateApprovalStatusAsync(map);
            return response;
        }
        public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsApprovalStatusAsync()
        {
            var mappings = await _repository.GetAllAsync();

            var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.Reject)
                    .Select(m => new MappingUserAppointmentResponse
                    {
                        MappingUserAppointmentId = m.MappingUserAppointmentId,
                        UserId = m.UserId,
                        FullName = m.User != null ? m.User.FullName : "N/A", // Kiểm tra null
                        AppointmentId = m.AppointmentId,
                        AppointmentTitle = m.Appointment != null ? m.Appointment.Title : "N/A", // Kiểm tra null
                        AppointmentDate = m.Appointment.AppointmentDate,
                        CustomerName = m.Appointment != null && m.Appointment.Customer != null
                    ? m.Appointment.Customer.FullName
                    : "N/A"
                    });

            return response.ToList();
        }
    }
}