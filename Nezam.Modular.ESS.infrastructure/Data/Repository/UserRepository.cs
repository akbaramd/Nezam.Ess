using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserRepository : EfCoreBonRepository<UserEntity, BonUserId, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }

    protected override IQueryable<UserEntity> PrepareQuery(DbSet<UserEntity> dbSet)
    {
        return dbSet
            .Include(x=>x.Roles)
            .Include(x=>x.Engineer)
            .Include(x=>x.Employer)
            .Include(x => x.VerificationTokens);
    }

}