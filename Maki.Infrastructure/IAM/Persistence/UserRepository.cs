using Maki.Domain.IAM.Models.Entities;
using Maki.Domain.IAM.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Maki.Infrastructure.IAM.Persistence;

public class UserRepository : IUserRepository
{
    private readonly MakiContext _makiContext;
    
    public UserRepository(MakiContext makiContext)
    {
        _makiContext = makiContext;
    }
    
    public async Task<int> RegisterAsync(User user)
    {
        _makiContext.Users.Add(user);
        await _makiContext.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await _makiContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        return user;
    }
}