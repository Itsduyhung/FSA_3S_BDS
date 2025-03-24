using FSA_3S.Models.Entities;

namespace FSA_3S.Repositories.Interface
{
    public interface ICustomerRepository
    {
        Task<CustomerEntity?> GetByIdentityNumberAsync(string identityNumber);
        Task<int> AddCustomerAsync(CustomerEntity customer);
    }
}