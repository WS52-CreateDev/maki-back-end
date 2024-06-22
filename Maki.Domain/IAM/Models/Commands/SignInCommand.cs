namespace Maki.Domain.IAM.Models.Commands;

public record SignInCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}