using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class DocumentReadOnlyRepository : EfCoreReadonlyRepository<DocumentAggregateRoot,DocumentId, IdentityDbContext>, IDocumentReadOnlyRepository
{

    public async Task<DocumentAggregateRoot?> GetEmptyDraftByUserAsync(UserId UserId)
    {
        return  await PrepareQuery((await GetDbContextAsync()).Set<DocumentAggregateRoot>())
            .Where(d => d.OwnerUserId == UserId 
                        && d.Status == DocumentStatus.Draft
                        && !d.Attachments.Any() 
                        && !d.Referrals.Any())
            .FirstOrDefaultAsync();
    }
    
    protected override IQueryable<DocumentAggregateRoot> PrepareQuery(DbSet<DocumentAggregateRoot> dbSet)
    {
        return dbSet.Include(x => x.Attachments)
            .Include(x => x.Referrals)
            .Include(x => x.ActivityLogs);
    }
}

public class DocumentRepository : EfCoreBonRepository<DocumentAggregateRoot,DocumentId, IdentityDbContext>, IDocumentRepository
{


}