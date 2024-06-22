using Maki.Domain.IAM.Models.Commands;

namespace Maki.Domain.IAM.Services;

public interface IUserCommandService
{
    Task<string> Handle(SignInCommand command);
    Task<int> Handle(SignUpCommand command);
}