using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserRepository : EfCoreBonRepository<UserEntity, BonUserId, IdEntityDbContext>, IUserRepository
{
    public UserRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }

    protected override IQueryable<UserEntity> PrepareQuery(DbSet<UserEntity> dbSet)
    {
        return dbSet.Include(x => x.Roles)
            .Include(x=>x.Engineer)
            .Include(x=>x.Employer)
            .Include(x => x.VerificationTokens);
    }

}