using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserRepository : EntityFrameworkRepository<UserEntity,AppDbContext>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public Task<UserEntity?> GetByUserNameAsync(UserNameValue userName)
    {
        return FindOneAsync(x => x.UserName.Value == userName.Value);
    }

    public Task<UserEntity?> GetByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}