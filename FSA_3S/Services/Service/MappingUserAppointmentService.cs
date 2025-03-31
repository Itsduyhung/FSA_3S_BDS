using System.Collections.Generic;
using System.Linq; // Đảm bảo đã thêm using này
using System.Threading.Tasks;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using FSA_3S.Helpers;
using FSA_3S.Enum;
using FSA_3S.Models.Requests;
using System.Xml;
namespace FSA_3S.Services.Service
{
    

    public class MappingUserAppointmentService(IMappingUserAppointmentRepository _repository, IHttpContextAccessor _httpContextAccessor, IAppointmentService _appointmentService) : IMappingUserAppointmentService
    {
        
   
           
public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsAsync()
    {
        var mappings = await _repository.GetAllAsync();

        var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.Approved)
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
        int  createBy = UserIdHelper.GetUserId(_httpContextAccessor)
            ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

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
                ApprovalStatus = ApprovalStatusEnum.NotApproved,
                CreatedBy = createBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            try
            {
                var result = await _repository.AddAsync(mapping);
                return new MappingUserAppointmentResponse
                {
                    MappingUserAppointmentId = result.MappingUserAppointmentId,
                    UserId = result.UserId,
                    AppointmentId = result.AppointmentId,
                    ApprovalStatus= result.ApprovalStatus,
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
            var map =await _repository.GetByIdForPutAsync(id);
            if (map == null) return null;
            map.ApprovalStatus = (ApprovalStatusEnum)request.ApprovalStatus;
            map.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateApprovalStatusAsync(map);
            var respon = new MappingUserAppointmentResponse
            {
                ApprovalStatus = map.ApprovalStatus,
                MappingUserAppointmentId = map.MappingUserAppointmentId,
                UserId = map.UserId,
                AppointmentId = map.AppointmentId,
                CreatedBy = map.CreatedBy,
                UpdatedBy = map.UpdatedBy,
                CreatedAt = map.CreatedAt,
                UpdatedAt = map.UpdatedAt

            }; return respon;
        }

        public async Task<IEnumerable<MappingUserAppointmentResponse>> GetAllMappingsApprovalStatusAsync()
        {
            var mappings = await _repository.GetAllAsync();

            var response = mappings.Where(m => m.Appointment != null && m.ApprovalStatus == ApprovalStatusEnum.NotApproved)
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