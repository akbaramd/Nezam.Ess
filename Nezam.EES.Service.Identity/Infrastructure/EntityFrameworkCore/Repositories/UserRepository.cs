using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.Repositories;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;

public class UserRepository : EntityFrameworkRepository<UserEntity,IdentityDbContext>, IUserRepository
{
    public UserRepository(IUnitOfWorkManager work) : base(work)
    {
    }

    public Task<UserEntity?> GetByUserNameAsync(UserNameId userName)
    {
        return FindOneAsync(x => x.UserName == userName);
    }

    public Task<UserEntity?> GetByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}