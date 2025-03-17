using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface IContractRepository
    {
        /// <summary>
        /// API POST Contract
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        Task<ContractEntity> CreateContractAsync(ContractEntity contract);
    }
}