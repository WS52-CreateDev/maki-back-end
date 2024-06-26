using AutoMapper;
using Maki.Domain.Artisan.Models.Aggregates;
using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Entities;

namespace Maki.Presentation.Mapper;

public class RequestToModels : Profile
{
    public RequestToModels()
    {
        CreateMap<CreateProductCommand, ProductA>();
        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<RegisterArtisanCommand, ArtisanA>();
        CreateMap<UpdateArtisanCommand, ArtisanA>();
    }
    
}