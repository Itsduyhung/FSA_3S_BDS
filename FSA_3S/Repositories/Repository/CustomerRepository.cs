using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerEntity?> GetByIdentityNumberAsync(string identityNumber)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.CCCD == identityNumber);
    }

    public async Task<int> AddCustomerAsync(CustomerEntity customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer.CustomerId;
    }
}