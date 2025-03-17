using FSA_3S.Models.Requests;
using FSA_3S.Models.Entities;

namespace FSA_3S.Services.Interface
{
    public interface IContractService
    {
        Task<ContractEntity> CreateContractAsync(ContractRequest request);
    }
}