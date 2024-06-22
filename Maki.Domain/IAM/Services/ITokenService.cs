using Maki.Domain.IAM.Models.Entities;

namespace Maki.Domain.IAM.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}