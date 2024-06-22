using Maki.Domain.IAM.Models.Entities;

namespace Maki.Domain.IAM.Repositories;

public interface IUserRepository
{
    Task<int> RegisterAsync(User user);
    Task<User?> GetUserByUsernameAsync(string username);
}