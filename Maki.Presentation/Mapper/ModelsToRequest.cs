using AutoMapper;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Entities;
using Maki.Domain.DesignRequest.Models.Response;

namespace Maki.Presentation.Mapper;

public class ModelsToRequest : Profile
{
    public ModelsToRequest()
    {
        CreateMap<ProductA, CreateProductCommand>();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<DesignRequest, CreateDesignRequestCommand>();
        CreateMap<DesignRequest, UpdateDesignRequestCommand>();
        CreateMap<DesignRequest, DeleteDesignRequestCommand>();
    }
    
}