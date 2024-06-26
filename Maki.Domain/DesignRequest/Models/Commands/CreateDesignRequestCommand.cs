using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.DesignRequest.Models.Commands;

public class CreateDesignRequestCommand
{
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
}