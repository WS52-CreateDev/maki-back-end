using AutoMapper;
using Maki.Domain.Artisan.Models.Aggregates;
using Maki.Domain.Artisan.Models.Response;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.Product.Models.Response;

namespace Maki.Presentation.Mapper;

public class ModelsToResponse : Profile
{
    public ModelsToResponse()
    {
        CreateMap<Domain.Product.Models.Aggregates.ProductA, ProductResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));;
        CreateMap<Category, CategoryResponse>();
        CreateMap<ArtisanA, ArtisanResponse>();
    }
}