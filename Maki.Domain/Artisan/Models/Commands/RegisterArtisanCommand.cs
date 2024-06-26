using System.ComponentModel.DataAnnotations;

namespace Maki.Domain.Artisan.Models.Commands;

public class RegisterArtisanCommand
{
    [Required] public string Name { get; set; }
    [Required] public string Surname { get; set; }
    [Required] public string Phone { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string Email { get; set; }
    public string Photo { get; set; }
    public int Age { get; set; }
    public string Province { get; set; } 
    public string Info { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string BusinessName { get; set; }
    [Required] public string BusinessAddress { get; set; }
}