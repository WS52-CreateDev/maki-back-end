using Maki.Domain.Artisan.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.Artisan.Persistence;

using Maki.Domain.Artisan.Models.Aggregates;

public class ArtisanRepository: IArtisanRepository
{

    private readonly MakiContext _makiContext;

    public ArtisanRepository(MakiContext makiContext)
    {
        _makiContext = makiContext;
    }


    public async Task<ArtisanA> SaveAsync(ArtisanA artisan)
    {
        await _makiContext.Artisans.AddAsync(artisan);
        await _makiContext.SaveChangesAsync();
        return artisan;
    }

    public async Task<ArtisanA> GetByEmailAndPasswordAsync(string email, string password)
    {
        return await _makiContext.Artisans
            .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
    }

    public async Task<bool> UpdateAsync(ArtisanA artisan)
    {
        var existingArtisan = await _makiContext.Artisans.FindAsync(artisan.Id);
        if (existingArtisan == null) throw new KeyNotFoundException(" Artisan not found ");
        _makiContext.Entry(existingArtisan).CurrentValues.SetValues(artisan);
        await _makiContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var artisan = await _makiContext.Artisans.FindAsync(id);
        if (artisan == null) return false;
        _makiContext.Artisans.Remove(artisan);
        await _makiContext.SaveChangesAsync();
        return true;
    }

    public async Task<ArtisanA> GetByIdAsync(int id)
    {
        return await _makiContext.Artisans.FindAsync(id);
    }

    public async Task<IEnumerable<ArtisanA>> GetAllAsync()
    {
        return await _makiContext.Artisans.ToListAsync();
    }
}