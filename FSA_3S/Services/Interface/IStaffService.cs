using FSA_3S.DTOs;
using FSA_3S.Enum;
using FSA_3S.Models.Entities;

namespace FSA_3S.Services.Interface
{
    public interface IStaffService
    {
        /// <summary>
        /// API GET User role Staff
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<IEnumerable<StaffDTO>> GetStaffByRoleAsync(string role);

        /// <summary>
        /// API Uncable for User rolle Staff
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<UnableStaffResponse> ToggleAccountStatusAsync(int userId);
    }
}