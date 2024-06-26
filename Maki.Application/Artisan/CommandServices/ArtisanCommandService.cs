using System.Data;
using AutoMapper;
using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Artisan.Models.Response;
using Maki.Domain.Artisan.Repositories;
using Maki.Domain.Artisan.Services;

using Maki.Domain.Artisan.Models.Aggregates;

public class ArtisanCommandService : IArtisanCommandService
{

    private readonly IArtisanRepository _artisanRepository;
    private readonly IMapper _mapper;


    public ArtisanCommandService(IArtisanRepository artisanRepository, IMapper mapper)
    {
        _artisanRepository = artisanRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(RegisterArtisanCommand command)
    {
        var artisan = _mapper.Map<RegisterArtisanCommand, ArtisanA>(command);
        var existingArtisan = await _artisanRepository.GetByEmailAndPasswordAsync(artisan.Email, artisan.Password);
        if (existingArtisan != null) throw new DuplicateNameException("Artisan already exists");

        var result = await _artisanRepository.SaveAsync(artisan);
        return result.Id;
    }

    public async Task<ArtisanResponse> Handle(LogInArtisanCommand command)
    {
        var artisan = await _artisanRepository.GetByEmailAndPasswordAsync(command.Email, command.Password);
        if (artisan == null) throw new UnauthorizedAccessException("Invalid Email");

        return _mapper.Map<ArtisanA, ArtisanResponse>(artisan);
    }

    public async Task<bool> Handle(UpdateArtisanCommand command)
    {
        var existingArtisan = await _artisanRepository.GetByIdAsync(command.Id);
        if (existingArtisan == null) throw new KeyNotFoundException("Artisan not found");

        var artisan = _mapper.Map<UpdateArtisanCommand, ArtisanA>(command);
        return await _artisanRepository.UpdateAsync(artisan);

    }

    public async Task<bool> Handle(DeleteArtisanCommand command)
    {
        var existingArtisan = await _artisanRepository.GetByIdAsync(command.Id);
        if (existingArtisan == null) throw new KeyNotFoundException("Artisan not found");

        return await _artisanRepository.DeleteAsync(command.Id);
    }
}