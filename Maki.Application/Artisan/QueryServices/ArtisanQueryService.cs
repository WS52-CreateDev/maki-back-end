using AutoMapper;
using Maki.Domain.Artisan.Models.Queries;
using Maki.Domain.Artisan.Models.Response;
using Maki.Domain.Artisan.Repositories;
using Maki.Domain.Artisan.Services;

namespace Maki.Application.Artisan.QueryServices;

public class ArtisanQueryService: IArtisanQueryService
{
    private readonly IArtisanRepository _artisanRepository;
    private readonly IMapper _mapper;

    public ArtisanQueryService(IArtisanRepository artisanRepository, IMapper mapper)
    {
        _artisanRepository = artisanRepository;
        _mapper = mapper;
    }

    public async Task<List<ArtisanResponse>?> Handle(GetAllArtisansQuery query)
    {
        var artisans = await _artisanRepository.GetAllAsync();
        return _mapper.Map<List<ArtisanResponse>>(artisans);
    }

    public async Task<ArtisanResponse?> Handle(GetArtisanByEmailAndPasswordQuery query)
    {
        var artisan = await _artisanRepository.GetByEmailAndPasswordAsync(query.Email, query.Password);
        if (artisan == null) throw new KeyNotFoundException("Artisan not found");
        return _mapper.Map<ArtisanResponse>(artisan);
    }

    public async Task<ArtisanResponse?> Handle(GetArtisanByIdQuery query)
    {
        var artisan = await _artisanRepository.GetByIdAsync(query.id);
        if (artisan == null) throw new KeyNotFoundException("Artisan not found");
        return _mapper.Map<ArtisanResponse>(artisan);
    }
}