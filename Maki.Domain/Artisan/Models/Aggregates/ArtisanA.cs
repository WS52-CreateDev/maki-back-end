using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Maki.Domain.Artisan.Models.Aggregates;

public class ArtisanA
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    [MaxLength(9)]
    [MinLength(9)]
    public string Phone { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address.")]
    public string Email { get; set; }
    [Required]
    public string Photo { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public string Province { get; set; }
    [Required]
    public string Info { get; set; }
    [Required]
    [MaxLength(12)]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    public string BusinessName { get; set; }
    [Required]
    public string BusinessAddress { get; set; }
}