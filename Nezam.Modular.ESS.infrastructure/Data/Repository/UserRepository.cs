using System.Linq.Expressions;
using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.infrastructure.Data.Repository;

public class UserRepository : EfCoreRepository<UserEntity, UserId, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }


    public new async Task<UserEntity?> FindOneAsync(Expression<Func<UserEntity,bool>> specification)
    {
        var dbContet = await GetDbContextAsync();
        var d = await dbContet.Users
            .Include(x => x.Roles)
            .Include(x=>x.VerificationTokens)
            .FirstOrDefaultAsync(specification);
        return d;
    }
}