using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.Product.Persistence;

public class ProductRepository : IProductRepository
{
    private MakiContext _makiContext;

    public ProductRepository(MakiContext makiContext)
    {
        _makiContext = makiContext;
    }

    public async Task<List<ProductA>> GetAllAsync()
    {
        var result = await _makiContext.Products
            .Include(p => p.Category) 
            .Where(p=> p.IsActive)
            .ToListAsync();
        return result;
    }

    public async Task<ProductA> GetByIdAsync(int id)
    {
        return await _makiContext.Products.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
    }

    public async Task<ProductA> GetByNameAsync(string name)
    {
        return await _makiContext.Products.Where(p=> p.Name == name && p.IsActive).FirstOrDefaultAsync();
    }
    
    public async Task<int> SaveAsync(ProductA data)
    {
        var strategy = _makiContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _makiContext.Database.BeginTransactionAsync();
            await _makiContext.Products.AddAsync(data);
            await _makiContext.SaveChangesAsync();
            await transaction.CommitAsync();
        });
        return data.Id;
    }
    
    public async Task<bool> UpdateAsync(ProductA data, int id)
    {
        var existingProduct = await _makiContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        existingProduct.Name = data.Name;
        existingProduct.Description = data.Description;
        existingProduct.Price = data.Price;
        existingProduct.Image = data.Image;
        existingProduct.Width = data.Width;
        existingProduct.Height = data.Height;
        existingProduct.Depth = data.Depth;
        existingProduct.Material = data.Material;
        
        _makiContext.Products.Update(existingProduct);
        await _makiContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingProduct = await _makiContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        existingProduct.IsActive = false;
        _makiContext.Products.Update(existingProduct);
        await _makiContext.SaveChangesAsync();
        return true;
    }
    
}