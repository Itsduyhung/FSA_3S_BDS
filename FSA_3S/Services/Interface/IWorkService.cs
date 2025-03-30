using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;

namespace FSA_3S.Services.Interface
{
    public interface IWorkService
    {
        Task<WorkEntity?> CreateWorkAsync(WorkRequest request);
        Task<IEnumerable<WorkEntity>> GetAllWorksAsync();
        Task<WorkEntity?> UpdateWorkAsync(int workId, WorkRequest request);
        Task<bool> DeleteWorkAsync(int workId);
    }
}