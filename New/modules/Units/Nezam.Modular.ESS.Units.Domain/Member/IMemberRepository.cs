using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;

namespace Nezam.Modular.ESS.Units.Domain.Member;

public interface IMemberRepository : IBonRepository<MemberEntity>
{
    public Task<MemberEntity?> FindByUserIdAsync(UserId userId);
}