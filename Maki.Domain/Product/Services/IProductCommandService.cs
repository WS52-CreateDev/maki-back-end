using Maki.Domain.Product.Models.Commands;

namespace Maki.Domain.Product.Services;

public interface IProductCommandService
{
    Task<int> Handle(CreateProductCommand command);
    Task<bool> Handle(UpdateProductCommand command);
    Task<bool> Handle(DeleteProductCommand command);
}