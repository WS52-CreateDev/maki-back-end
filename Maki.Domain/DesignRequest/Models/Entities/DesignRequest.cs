using System.ComponentModel.DataAnnotations;
using Maki.Domain.Artisan.Models.Aggregates;
using Maki.Domain.IAM.Models.Entities;

namespace Maki.Domain.DesignRequest.Models.Entities;

public class DesignRequest
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Characteristics { get; set; }

    [Required]
    public string Photo { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public int ArtisanId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;

    public ArtisanA Artisan { get; set; }
}