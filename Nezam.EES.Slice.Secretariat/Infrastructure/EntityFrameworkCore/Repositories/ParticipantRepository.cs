using Microsoft.EntityFrameworkCore;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Repositories;

public class ParticipantRepository : EfCoreRepository<Participant, ISecretariatDbContext>, IParticipantRepository
{
    public ParticipantRepository(IEfCoreDbContextProvider<ISecretariatDbContext> contextProvider) : base(contextProvider)
    {
    }

    public Task<Participant?> FindByUserIdAsync(UserId userId)
    {
        return FindOneAsync(x=>x.UserId == userId);  
    }
}