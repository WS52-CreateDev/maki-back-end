using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.Artisan.Models.Commands;

public class RegisterArtisanCommand
{
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
    [Required] public string Phone { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Photo { get; set; }
    [Required] public int Age { get; set; }
    [Required] public string Province { get; set; }
    [Required] public string Info { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string BusinessName { get; set; }
    [Required] public string BusinessAddress { get; set; }
}