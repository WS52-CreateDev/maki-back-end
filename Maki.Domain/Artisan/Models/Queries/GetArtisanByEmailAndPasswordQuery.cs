namespace Maki.Domain.Artisan.Models.Queries;

public record GetArtisanByEmailAndPasswordQuery (string Email, string Password)
{
    
}