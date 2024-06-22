using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.Product.Models.Commands;

public class UpdateProductCommand
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int Price { get; set; }
    [Required] public string Image { get; set; }
    [Required] public string Width { get; set; }
    [Required] public string Height { get; set; }
    [Required] public string Depth { get; set; }
    [Required] public string Material { get; set; }
    [Required] public int ArtisanId { get; set; }
    [Required] public int CategoryId { get; set; }
}