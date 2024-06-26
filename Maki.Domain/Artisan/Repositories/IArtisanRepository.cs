namespace Maki.Domain.Artisan.Repositories;

using Maki.Domain.Artisan.Models.Aggregates;

public interface IArtisanRepository
{
    Task<ArtisanA> SaveAsync(ArtisanA artisan);
    Task<ArtisanA> GetByEmailAndPasswordAsync(string email, string password);
    Task<bool> UpdateAsync(ArtisanA artisan);
    Task<bool> DeleteAsync(int id);
    Task<ArtisanA> GetByIdAsync(int id);
    Task<IEnumerable<ArtisanA>> GetAllAsync();
    
}