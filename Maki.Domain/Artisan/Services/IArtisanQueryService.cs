using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Artisan.Models.Queries;
using Maki.Domain.Artisan.Models.Response;

namespace Maki.Domain.Artisan.Services;

public interface IArtisanQueryService
{
   Task<List<ArtisanResponse>?> Handle(GetAllArtisansQuery query);
   Task<ArtisanResponse?> Handle(GetArtisanByEmailAndPasswordQuery query);
   Task<ArtisanResponse?> Handle(GetArtisanByIdQuery query);

}