using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.DTOs;

namespace FSA_3S.Services.Interface
{
    public interface IContractService
    {
        /// <summary>
        /// API Post Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ContractResponse> CreateContractAsync(ContractRequest request);
        Task ValidateContractRequest(ContractRequest request);

        /// <summary>
        /// API Change status of Contract which expried
        /// </summary>
        /// <returns></returns>
        Task UpdateContractStatusAsync();

        /// <summary>
        /// API Get All Contract
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ContractDTO>> GetAllContractsAsync();
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        Task<bool> DeleteContractAsync(int contractId);
    }
}