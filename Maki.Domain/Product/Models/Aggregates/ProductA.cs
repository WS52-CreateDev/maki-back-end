using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Maki.Domain.Product.Models.Entities;

namespace Maki.Domain.Product.Models.Aggregates;

public class ProductA
{
    public int Id { get; set; }
    
    [MaxLength(30)]
    [Required]
    public string Name { get; set; }
    public DateTime PublicationDate { get; set; } = DateTime.Now;
    [Required]
    public int Price { get; set; }
    [Required]
    public string Image { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Width { get; set; }
    [Required]
    public string Height { get; set; }
    [Required]
    public string Depth { get; set; }
    [Required]
    public string Material { get; set; }
    [Required]
    public int ArtisanId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    public bool IsActive { get; set; } = true;
}