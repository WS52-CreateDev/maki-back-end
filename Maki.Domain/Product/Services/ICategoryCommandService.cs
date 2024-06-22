using Maki.Domain.Product.Models.Commands;

namespace Maki.Domain.Product.Services;

public interface ICategoryCommandService
{
    Task<int> Handle(CreateCategoryCommand command);
    Task<bool> Handle(UpdateCategoryCommand command);
    Task<bool> Handle(DeleteCategoryCommand command);
}