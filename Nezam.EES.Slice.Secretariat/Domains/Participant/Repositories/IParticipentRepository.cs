using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.Repositories;

namespace Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;

public interface IParticipantRepository : IRepository<Participant>
{
    /// <summary>
    /// Finds participants by their name.
    /// </summary>
    Task<Participant?> FindByUserIdAsync(UserId userId);


}
