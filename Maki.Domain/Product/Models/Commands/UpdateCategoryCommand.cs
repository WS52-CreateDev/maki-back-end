using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.Product.Models.Commands;

public class UpdateCategoryCommand
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; }
}