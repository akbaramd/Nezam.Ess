using System.Linq.Expressions;
using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserRepository : EfCoreRepository<UserEntity, UserId, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }

    protected override IQueryable<UserEntity> PrepareQuery(DbSet<UserEntity> dbSet)
    {
        return dbSet.Include(x => x.Roles)
            .Include(x => x.VerificationTokens);
    }


}