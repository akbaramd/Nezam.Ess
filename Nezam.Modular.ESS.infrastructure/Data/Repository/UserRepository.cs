using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserRepository : EfCoreBonRepository<UserEntity,IdentityDbContext>, IUserRepository
{

    protected override IQueryable<UserEntity> PrepareQuery(DbSet<UserEntity> dbSet)
    {
        return dbSet
            .Include(x => x.VerificationTokens);
    }

    public Task<UserEntity?> GetByUserNameAsync(UserNameValue userName)
    {
        return FindOneAsync(x => x.UserName == userName);
    }

    public Task<UserEntity?> GetByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}