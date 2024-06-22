using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.Product.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.Product.Persistence;

public class CategoryRepository : ICategoryRepository
{
    private MakiContext _makiContext;
    
    public CategoryRepository(MakiContext makiContext)
    {
        _makiContext = makiContext;
    }
    
    public async Task<List<Category>> GetAllAsync()
    {
        var result = await _makiContext.Categories.Where(c=> c.IsActive).ToListAsync();
        return result;
    }
    
    public async Task<Category> GetByIdAsync(int id)
    {
        return await _makiContext.Categories.Where(c => c.Id == id && c.IsActive).FirstOrDefaultAsync();
    }
    
    public async Task<Category> GetByNameAsync(string name)
    {
        return await _makiContext.Categories.Where(c=> c.Name == name && c.IsActive).FirstOrDefaultAsync();
    }

    public async Task<List<ProductA>> GetProductsByCategoryAsync(string name)
    {
        return await _makiContext.Products.Where(p => p.Category.Name == name && p.IsActive).ToListAsync();
    }
    
    public async Task<int> SaveAsync(Category data)
    {
        var strategy = _makiContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _makiContext.Database.BeginTransactionAsync();
            await _makiContext.Categories.AddAsync(data);
            await _makiContext.SaveChangesAsync();
            await transaction.CommitAsync();
        });
        return data.Id;
    }
    
    public async Task<bool> UpdateAsync(Category data, int id)
    {
        var existingCategory = await _makiContext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        existingCategory.Name = data.Name;
        
        _makiContext.Categories.Update(existingCategory);
        await _makiContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var existingCategory = await _makiContext.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        existingCategory.IsActive = false;
        _makiContext.Categories.Update(existingCategory);
        await _makiContext.SaveChangesAsync();
        return true;
    }
}