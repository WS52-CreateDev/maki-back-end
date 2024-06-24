using AutoMapper;
using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Queries;
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
        CreateMap<UpdateCustomerCommand, Customer>();
    }

}