using FSA_3S.Services.Interface;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Repositories.Interface;

namespace FSA_3S.Services.Service
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        /// <summary>
        /// API Post Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ContractEntity> CreateContractAsync(ContractRequest request)
        {
            var contract = new ContractEntity
            {
                RealEstateId = request.RealEstateId,
                CustomerId = request.CustomerId,
                ContractType = request.ContractType,
                ContractStatus = request.ContractStatus,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = DateTime.UtcNow,
                UpdatedBy = request.UpdatedBy,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _contractRepository.CreateContractAsync(contract);
        }
    }
}