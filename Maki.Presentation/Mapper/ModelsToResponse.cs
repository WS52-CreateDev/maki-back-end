using AutoMapper;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.DesignRequest.Models.Entities;
using Maki.Domain.DesignRequest.Models.Response;

namespace Maki.Presentation.Mapper;

public class ModelsToResponse : Profile
{
    public ModelsToResponse()
    {
        CreateMap<Domain.Product.Models.Aggregates.ProductA, ProductResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));;
        CreateMap<Category, CategoryResponse>();
        CreateMap<DesignRequest, DesignRequestResponse>();
    }
}