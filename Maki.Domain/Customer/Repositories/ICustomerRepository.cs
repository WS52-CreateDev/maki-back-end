namespace Maki.Domain.Customer.Repositories;

using Maki.Domain.Customer.Models.Entities;

public interface ICustomerRepository
{
    Task<Customer> RegisterAsync(Customer customer);
    Task<Customer> GetByEmailAndPasswordAsync(string email, string password);
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
}