using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;

namespace Maki.Domain.Product.Services;

public interface ICategoryQueryService
{
    Task<List<CategoryResponse>?> Handle(GetAllCategoriesQuery query);
    Task<CategoryResponse?> Handle(GetCategoryByIdQuery query);
}