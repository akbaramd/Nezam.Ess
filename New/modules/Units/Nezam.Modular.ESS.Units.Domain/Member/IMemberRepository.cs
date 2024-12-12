using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.Modular.ESS.Units.Domain.Member;

public interface IMemberRepository : IRepository<MemberEntity>
{
    public Task<MemberEntity?> FindByUserIdAsync(UserId userId);
}