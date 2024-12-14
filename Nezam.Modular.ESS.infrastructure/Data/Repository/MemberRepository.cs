using Nezam.Modular.ESS.Units.Domain.Member;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class MemberRepository : EntityFrameworkRepository<MemberEntity,AppDbContext> ,IMemberRepository
{
    public MemberRepository(IUnitOfWorkManager work) : base(work)
    {
    }

    public Task<MemberEntity?> FindByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x => x.UserId == userId);
    }
}