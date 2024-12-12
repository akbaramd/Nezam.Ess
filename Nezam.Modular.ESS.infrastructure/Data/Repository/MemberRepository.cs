using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Units.Domain.Member;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class MemberRepository : EntityFrameworkRepository<MemberEntity,AppDbContext> ,IMemberRepository
{
    public MemberRepository(AppDbContext context) : base(context)
    {
    }

    public Task<MemberEntity?> FindByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}