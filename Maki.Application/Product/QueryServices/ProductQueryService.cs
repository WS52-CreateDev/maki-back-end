using AutoMapper;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;

namespace Maki.Application.Product.QueryServices;

public class ProductQueryService : IProductQueryService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductQueryService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<List<ProductResponse>?> Handle(GetAllProductsQuery query)
    {
        var data = await _productRepository.GetAllAsync();
        var result = _mapper.Map<List<ProductA>, List<ProductResponse>>(data);
        return result;
    }
    
    public async Task<ProductResponse?> Handle(GetProductByIdQuery query)
    {
        var data = await _productRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<ProductA, ProductResponse>(data);
        return result;
    }
}