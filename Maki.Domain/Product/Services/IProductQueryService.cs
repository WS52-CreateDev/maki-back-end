using Maki.Domain.Product.Models.Queries;
using Maki.Domain.Product.Models.Response;

namespace Maki.Domain.Product.Services;

public interface IProductQueryService
{
    Task<List<ProductResponse>?> Handle(GetAllProductsQuery query);
    Task<ProductResponse?> Handle(GetProductByIdQuery query);
}