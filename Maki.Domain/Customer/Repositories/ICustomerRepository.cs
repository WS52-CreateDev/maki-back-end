namespace Maki.Domain.Customer.Repositories;

public interface ICustomerRepository
{
    Task<Models.Queries.Customer> RegisterAsync(Models.Queries.Customer customer);
    Task<Models.Queries.Customer> GetByEmailAndPasswordAsync(string email, string password);
    Task<Models.Queries.Customer> GetByIdAsync(int id);
    Task<IEnumerable<Models.Queries.Customer>> GetAllAsync();
    Task<bool> UpdateAsync(Models.Queries.Customer customer);
    Task<bool> DeleteAsync(int id);
}