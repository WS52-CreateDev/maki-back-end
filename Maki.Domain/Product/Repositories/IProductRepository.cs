
using Maki.Domain.Product.Models.Aggregates;

namespace Maki.Domain.Product.Repositories;

public interface IProductRepository
{
    Task<List<ProductA>> GetAllAsync();
    
    Task<ProductA> GetByIdAsync(int id);
    
    Task<ProductA> GetByNameAsync(string name);
    
    Task<int> SaveAsync(ProductA data);

    Task<bool> UpdateAsync(ProductA data, int id);

    Task<bool> DeleteAsync(int id);
    
}