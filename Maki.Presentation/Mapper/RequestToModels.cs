using AutoMapper;
using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Entities;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Entities;

namespace Maki.Presentation.Mapper;

public class RequestToModels : Profile
{
    public RequestToModels()
    {
        CreateMap<CreateProductCommand, ProductA>();
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<CreateDesignRequestCommand, DesignRequest>();
        CreateMap<UpdateDesignRequestCommand, DesignRequest>();
        CreateMap<DeleteDesignRequestCommand, DesignRequest>();
        CreateMap<RegisterCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
    
}