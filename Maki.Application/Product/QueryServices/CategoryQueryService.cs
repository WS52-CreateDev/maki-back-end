using AutoMapper;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;

namespace Maki.Application.Product.QueryServices;

public class CategoryQueryService : ICategoryQueryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CategoryQueryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CategoryResponse>?> Handle(GetAllCategoriesQuery query)
    {
        var data = await _categoryRepository.GetAllAsync();
        var result = _mapper.Map<List<Category>, List<CategoryResponse>>(data);
        return result;
    }
    
    public async Task<CategoryResponse?> Handle(GetCategoryByIdQuery query)
    {
        var data = await _categoryRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Category, CategoryResponse>(data);
        return result;
    }
}