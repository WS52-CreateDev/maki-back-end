using AutoMapper;
using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Queries;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Entities;

namespace Maki.Presentation.Mapper;

public class ModelsToRequest : Profile
{
    public ModelsToRequest()
    {
        CreateMap<ProductA, CreateProductCommand>();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<Customer, RegisterCustomerCommand>();
        CreateMap<Customer, UpdateCustomerCommand>();
        
    }
    
}