using Bonyan.Layer.Domain.Repository.Abstractions;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;

public interface IDocumentReadOnlyRepository : IBonReadOnlyRepository<DocumentAggregateRoot,DocumentId>
{
    Task<DocumentAggregateRoot?> GetEmptyDraftByUserAsync(BonUserId BonUserId);
}

public interface IDocumentRepository : IBonRepository<DocumentAggregateRoot,DocumentId> 
{
    
}