namespace Maki.Domain.Customer.Repositories;

public interface ICustomerRepository
{
    Task<Models.Entities.Customer> RegisterAsync(Models.Entities.Customer customer);
    Task<Models.Entities.Customer> GetByEmailAndPasswordAsync(string email, string password);
    Task<Models.Entities.Customer> GetByIdAsync(int id);
    Task<IEnumerable<Models.Entities.Customer>> GetAllAsync();
    Task<bool> UpdateAsync(Models.Entities.Customer customer);
    Task<bool> DeleteAsync(int id);
}