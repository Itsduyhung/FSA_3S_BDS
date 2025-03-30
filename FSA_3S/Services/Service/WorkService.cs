using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services.Interface;

namespace FSA_3S.Services.Service
{
    public class WorkService(IWorkRepository workRepository, IStaffRepository userRepository) : IWorkService
    {
        private readonly IWorkRepository _workRepository = workRepository;
        private readonly IStaffRepository _userRepository = userRepository;

        public async Task<WorkEntity?> CreateWorkAsync(WorkRequest request)
        {
            // Kiểm tra UserId có tồn tại và có role Staff không
            var user = await _userRepository.GetUserByIdAsync(request.UserId) ?? throw new ArgumentException("User không tồn tại.");
            if (user.Role != "Staff")
            {
                throw new UnauthorizedAccessException("Chỉ có thể tạo Work cho nhân viên (Staff).");
            }

            var work = new WorkEntity
            {
                UserId = request.UserId,
                WorkId = 0,
                TimeOfWork = request.TimeOfWork,
                DesWork = request.DesWork
            };

            await _workRepository.AddWorkAsync(work);
            return work;
        }
        /// <summary>
        /// API Get All Work
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WorkEntity>> GetAllWorksAsync()
        {
            return await _workRepository.GetAllWorksAsync();
        }

        /// <summary>
        /// API Put Work
        /// </summary>
        /// <param name="workId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<WorkEntity?> UpdateWorkAsync(int workId, WorkRequest request)
        {
            var work = await _workRepository.GetWorkByIdAsync(workId);
            if (work == null) return null;

            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null || user.Role != "Staff")
            {
                throw new UnauthorizedAccessException("Chỉ có thể cập nhật Work cho nhân viên (Staff).");
            }

            work.TimeOfWork = request.TimeOfWork;
            work.DesWork = request.DesWork;

            await _workRepository.UpdateWorkAsync(work);
            return work;
        }
        /// <summary>
        /// API Delete Work
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteWorkAsync(int workId)
        {
            var work = await _workRepository.GetWorkByIdAsync(workId);
            if (work == null)
            {
                return false;
            }

            await _workRepository.DeleteWorkAsync(work);
            return true;
        }
    }
}