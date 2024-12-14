using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.EES.Service.Identity.Domains.Users.Repositories;

public interface IUserRepository : IRepository<UserEntity>
{
   public Task<UserEntity> GetByUserNameAsync(UserNameId userName);
   public Task<UserEntity?> GetByUserIdAsync(UserId userId);
}