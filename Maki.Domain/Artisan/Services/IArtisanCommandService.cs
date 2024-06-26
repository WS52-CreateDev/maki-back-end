using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Artisan.Models.Response;

namespace Maki.Domain.Artisan.Services;

public interface IArtisanCommandService
{
    Task<int> Handle(RegisterArtisanCommand command);
    Task<ArtisanResponse> Handle(LogInArtisanCommand command);
    Task<bool> Handle(UpdateArtisanCommand command);
    Task<bool> Handle(DeleteArtisanCommand command);
}