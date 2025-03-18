using FSA_3S.Data;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models.Entities;
using FSA_3S.Models;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Repositories.Repository
{
    public class ContractRepository(AppDbContext context) : IContractRepository
    {
        private readonly AppDbContext _context = context;
        /// <summary>
        /// API Post Contract
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public async Task<ContractEntity> CreateContractAsync(ContractEntity contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }
        /// <summary>
        /// API Change Status Contract expried
        /// </summary>
        /// <returns></returns>
        public async Task<List<ContractEntity>> GetExpiredContractsAsync()
        {
            return await _context.Contracts
                .Where(c => c.EndDate <= DateTime.UtcNow && c.ContractStatus != Enum.ContractStatusEnum.Inactive)
                .ToListAsync();
        }
        public async Task UpdateContractsAsync(List<ContractEntity> contracts)
        {
            _context.Contracts.UpdateRange(contracts);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// API Get Contract (All)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ContractEntity>> GetAllContractAsync()
        {
            return await _context.Contracts.ToListAsync();
        }
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<ContractEntity?> GetContractByIdAsync(int contractId)
        {
            return await _context.Contracts.FindAsync(contractId);
        }

        public async Task DeleteContractAsync(ContractEntity contract)
        {
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }
    }
}