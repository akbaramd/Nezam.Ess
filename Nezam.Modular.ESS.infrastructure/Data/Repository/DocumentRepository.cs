using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.BonEnumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class DocumentReadOnlyRepository : EfCoreReadonlyRepository<DocumentAggregateRoot,DocumentId, IdEntityDbContext>, IDocumentReadOnlyRepository
{
    public DocumentReadOnlyRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
    public async Task<DocumentAggregateRoot?> GetEmptyDraftByUserAsync(BonUserId BonUserId)
    {
        return  await PrepareQuery((await GetDbContextAsync()).Set<DocumentAggregateRoot>())
            .Where(d => d.OwnerBonUserId == BonUserId 
                        && d.Status == DocumentStatus.Draft
                        && !d.Attachments.Any() 
                        && !d.Referrals.Any())
            .FirstOrDefaultAsync();
    }
    
    protected override IQueryable<DocumentAggregateRoot> PrepareQuery(DbSet<DocumentAggregateRoot> dbSet)
    {
        return dbSet.Include(x => x.Attachments)
            .Include(x => x.OwnerUser.Roles)
            .Include(x => x.OwnerUser.Employer)
            .Include(x => x.OwnerUser.Engineer)
            .Include(x => x.Referrals)
            .Include(x => x.ActivityLogs);
    }
}

public class DocumentRepository : EfCoreBonRepository<DocumentAggregateRoot,DocumentId, IdEntityDbContext>, IDocumentRepository
{
    public DocumentRepository(IdEntityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }

}