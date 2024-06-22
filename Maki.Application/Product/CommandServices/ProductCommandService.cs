using System.Data;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;
using AutoMapper;
using Maki.Domain.Product.Models.Aggregates;
using Maki.Domain.Product.Models.Commands;
using Maki.Shared;

namespace Maki.Application.Product.CommandServices;

public class ProductCommandService : IProductCommandService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductCommandService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand command)
    {
        var product = _mapper.Map<CreateProductCommand, ProductA>(command);
        var existingProduct = await _productRepository.GetByNameAsync(product.Name);
        if (existingProduct != null) throw new DuplicateNameException("Product already exists");
        
        return await _productRepository.SaveAsync(product);
    }

    public async Task<bool> Handle(UpdateProductCommand command)
    {
        var existingProduct = await _productRepository.GetByIdAsync(command.Id);
        var product = _mapper.Map<UpdateProductCommand, ProductA>(command);
        
        if (existingProduct == null) throw new ProductNotFoundException("Product not found");
        
        return await _productRepository.UpdateAsync(product, product.Id);
    }

    public async Task<bool> Handle(DeleteProductCommand command)
    {
        var existingProduct = await _productRepository.GetByIdAsync(command.Id);
        if (existingProduct == null) throw new ProductNotFoundException("Product not found");
        return await _productRepository.DeleteAsync(command.Id);
    }
}