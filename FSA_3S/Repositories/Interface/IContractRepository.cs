using FSA_3S.Models.Entities;
using FSA_3S.Models.Respone;

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

        /// <summary>
        /// API Change Contract Status auto
        /// </summary>
        /// <param name="contracts"></param>
        /// <returns></returns>
        Task UpdateContractsAsync(List<ContractEntity> contracts);
        Task<List<ContractEntity>> GetExpiredContractsAsync();
        Task<IEnumerable<ContractEntity>> GetAllContractAsync();
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        Task<ContractEntity?> GetContractByIdAsync(int contractId);
        Task DeleteContractAsync(ContractEntity contract);
    }
}