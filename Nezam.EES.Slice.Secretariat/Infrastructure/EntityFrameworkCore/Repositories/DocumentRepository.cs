using Microsoft.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Repositories;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Domains.Participant.Repositories;
using Payeh.SharedKernel.EntityFrameworkCore;
using Payeh.SharedKernel.EntityFrameworkCore.Domain;
using Payeh.SharedKernel.EntityFrameworkCore.UnitofWork;

namespace Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore.Repositories;

public class DocumentRepository : EfCoreRepository<DocumentAggregateRoot,ISecretariatDbContext>,IDocumentRepository
{
    public DocumentRepository(IEfCoreDbContextProvider<ISecretariatDbContext> contextProvider) : base(contextProvider)
    {
    }

    protected override IQueryable<DocumentAggregateRoot> PrepareQuery(DbSet<DocumentAggregateRoot> dbSet)
    {
        return base.PrepareQuery(dbSet)
            .Include(x=>x.Referrals).ThenInclude(x=>x.ReceiverParticipant)
            .Include(x=>x.Referrals).ThenInclude(x=>x.ReferrerParticipant)
            .Include(x=>x.Attachments);
    }
}