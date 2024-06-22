namespace Maki.Domain.IAM.Models.Commands;

public record SignUpCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}