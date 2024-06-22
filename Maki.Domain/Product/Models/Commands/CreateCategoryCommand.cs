using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.Product.Models.Commands;

public class CreateCategoryCommand
{
    [Required] public string Name { get; set; }
}