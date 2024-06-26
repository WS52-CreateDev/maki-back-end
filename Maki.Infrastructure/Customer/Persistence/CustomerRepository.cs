using Maki.Domain.Customer.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Maki.Domain.Customer.Models.Entities;
using Maki.Domain.Customer.Services;
using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.Customer.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly MakiContext _makiContext;

    public CustomerRepository(MakiContext makiContext)
    {
        _makiContext = makiContext;
    }

    public async Task<Domain.Customer.Models.Entities.Customer> RegisterAsync(
        Domain.Customer.Models.Entities.Customer customer)
    {
        await _makiContext.Customers.AddAsync(customer);
        await _makiContext.SaveChangesAsync();
        return customer;
    }

    public async Task<Domain.Customer.Models.Entities.Customer> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _makiContext.Customers
            .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);
    }

    public async Task<Domain.Customer.Models.Entities.Customer> GetByIdAsync(int id)
    {
        return await _makiContext.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Domain.Customer.Models.Entities.Customer>> GetAllAsync()
    {
        return await _makiContext.Customers.ToListAsync();
    }

    public async Task<bool> UpdateAsync(Domain.Customer.Models.Entities.Customer customer)
    {
        var existingCustomer = await _makiContext.Customers.FindAsync(customer.Id);
        if (existingCustomer == null)
            throw new KeyNotFoundException("Customer not found");

        _makiContext.Entry(existingCustomer).CurrentValues.SetValues(customer);
        await _makiContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _makiContext.Customers.FindAsync(id);
        if (customer == null) return false;

        _makiContext.Customers.Remove(customer);
        await _makiContext.SaveChangesAsync();
        return true;
    }

}
