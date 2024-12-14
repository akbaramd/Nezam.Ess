using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserRepository : IRepository<UserEntity>
{
   public Task<UserEntity> GetByUserNameAsync(UserNameValue userName);
   public Task<UserEntity?> GetByUserIdAsync(UserId userId);
}