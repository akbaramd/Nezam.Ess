using System.Linq.Expressions;
using Bonyan.Layer.Domain;
using Bonyan.Layer.Domain.Specifications;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data.Repository;

public class UserVerificationTokenRepository : EfCoreRepository<UserVerificationTokenEntity, IdentityDbContext>, IUserVerificationTokenRepository
{
    public UserVerificationTokenRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }


    public new async Task<UserVerificationTokenEntity?> FindOneAsync(Expression<Func<UserVerificationTokenEntity,bool>> specification)
    {
        var dbContet = await GetDbContextAsync();
        var d = await dbContet.UserVerificationToken
            .Include(x => x.User)
            .FirstOrDefaultAsync(specification);
        return d;
    }
}