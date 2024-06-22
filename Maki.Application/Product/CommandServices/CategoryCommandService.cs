using System.Data;
using AutoMapper;
using Maki.Domain.Product.Models.Commands;
using Maki.Domain.Product.Models.Entities;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;
using Maki.Shared;

namespace Maki.Application.Product.CommandServices;

public class CategoryCommandService : ICategoryCommandService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public CategoryCommandService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCategoryCommand command)
    {
        var category = _mapper.Map<CreateCategoryCommand, Category>(command);
        var existingCategory = await _categoryRepository.GetByNameAsync(category.Name);
        if (existingCategory != null) throw new DuplicateNameException("Category already exists");
        
        return await _categoryRepository.SaveAsync(category);
    }

    public async Task<bool> Handle(UpdateCategoryCommand command)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(command.Id);
        var category = _mapper.Map<UpdateCategoryCommand, Category>(command);
        
        if (existingCategory == null) throw new CategoryNotFoundException("Category not found");
        return await _categoryRepository.UpdateAsync(category, category.Id);
    }

    public async Task<bool> Handle(DeleteCategoryCommand command)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(command.Id);
        if (existingCategory == null) throw new CategoryNotFoundException("Category not found");
        return await _categoryRepository.DeleteAsync(command.Id);
    }
}