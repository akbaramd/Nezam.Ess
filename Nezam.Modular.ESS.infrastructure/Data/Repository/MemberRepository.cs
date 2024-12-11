using Bonyan.Layer.Domain;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Units.Domain.Member;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class MemberRepository : EfCoreBonRepository<MemberEntity,IdentityDbContext> ,IMemberRepository
{
    public Task<MemberEntity?> FindByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}