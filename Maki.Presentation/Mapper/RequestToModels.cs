using AutoMapper;
using Maki.Domain.Artisan.Models.Aggregates;
using Maki.Domain.Artisan.Models.Commands;
using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Entities;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Entities;
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
        CreateMap<RegisterCustomerCommand, Customer>();
        CreateMap<RegisterArtisanCommand, ArtisanA>();
        CreateMap<CreateDesignRequestCommand, DesignRequest>();
    }
    
}