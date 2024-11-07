using System.Linq.Expressions;
using Bonyan.Layer.Domain;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.IdEntity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class UserVerificationTokenRepository : EfCoreBonRepository<UserVerificationTokenEntity, IdEntityDbContext>, IUserVerificationTokenRepository
{
    public UserVerificationTokenRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }


    public new async Task<UserVerificationTokenEntity?> FindOneAsync(Expression<Func<UserVerificationTokenEntity,bool>> specification)
    {
        var dbContet = await GetDbContextAsync();
        var d = await dbContet.UserVerificationTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(specification);
        return d;
    }
}