using FSA_3S.Data;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models.Entities;
using FSA_3S.Models;

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
    }
}