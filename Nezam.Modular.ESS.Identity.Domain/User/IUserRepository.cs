using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Identity.Domain.User;

public interface IUserRepository : IBonRepository<UserEntity>
{
   public Task<UserEntity> GetByUserNameAsync(UserNameValue userName);
   public Task<UserEntity?> GetByUserIdAsync(UserId userId);
}