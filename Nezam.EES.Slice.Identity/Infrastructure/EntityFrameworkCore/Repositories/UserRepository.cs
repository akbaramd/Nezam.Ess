using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.Repositories;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Repositories;

public class UserRepository : EntityFrameworkRepository<UserEntity,IIdentityDbContext>, IUserRepository
{

    public Task<UserEntity?> GetByUserNameAsync(UserNameId userName)
    {
        return FindOneAsync(x => x.UserName == userName);
    }

    public Task<UserEntity?> GetByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }


    protected override IQueryable<UserEntity> PrepareQuery(DbSet<UserEntity> dbset)
    {
        return base.PrepareQuery(dbset).Include(x=>x.Roles).Include(x=>x.Tokens);
    }

    public UserRepository(IEfCoreDbContextProvider<IIdentityDbContext> contextProvider) : base(contextProvider)
    {
    }
}