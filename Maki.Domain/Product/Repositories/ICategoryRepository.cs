using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Entities;

namespace Maki.Domain.Product.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    
    Task<Category> GetByIdAsync(int id);
    
    Task<Category> GetByNameAsync(string name);
    
    Task<List<ProductA>> GetProductsByCategoryAsync(string name);
    
    Task<int> SaveAsync(Category data);
    
    Task<bool> UpdateAsync(Category data, int id);
    
    Task<bool> DeleteAsync(int id);
}