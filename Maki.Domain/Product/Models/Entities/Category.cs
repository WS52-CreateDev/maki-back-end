using System.ComponentModel.DataAnnotations;
using Maki.Domain.Product.Models.Aggregates;

namespace Maki.Domain.Product.Models.Entities;

public class Category
{
    public int Id { get; set; }
    
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public List<ProductA> Products { get; set; }
}