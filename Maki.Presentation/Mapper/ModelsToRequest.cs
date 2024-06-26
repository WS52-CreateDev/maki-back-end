﻿using AutoMapper;
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

public class ModelsToRequest : Profile
{
    public ModelsToRequest()
    {
        CreateMap<ProductA, CreateProductCommand>();
        CreateMap<Category, CreateCategoryCommand>();
        CreateMap<ArtisanA, RegisterArtisanCommand>();
        CreateMap<Customer, RegisterCustomerCommand>();
        CreateMap<DesignRequest, CreateDesignRequestCommand>();
    }
    
}