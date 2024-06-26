using Maki.Domain.IAM.Models.Entities;
using Maki.Domain.IAM.Queries;
using Maki.Domain.IAM.Repositories;
using Maki.Domain.IAM.Services;

namespace Maki.Application.IAM.QueryServices;

public class UserQueryService : IUserQueryService
{
    private readonly IUserRepository _userRepository;
    
    public UserQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> GetUserByIdAsync(GetUserByIdQuery query)
    {
        return  await _userRepository.GetUserByIdAsync(query.Id);
    }
}