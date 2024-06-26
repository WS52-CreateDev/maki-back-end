using Maki.Domain.IAM.Models.Entities;
using Maki.Domain.IAM.Queries;

namespace Maki.Domain.IAM.Services;

public interface IUserQueryService
{
    Task<User?> GetUserByIdAsync(GetUserByIdQuery query);
}