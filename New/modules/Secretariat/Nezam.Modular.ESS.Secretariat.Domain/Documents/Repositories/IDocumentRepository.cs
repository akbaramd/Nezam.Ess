using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;

public interface IDocumentReadOnlyRepository : IBonReadOnlyRepository<DocumentAggregateRoot,DocumentId>
{
    Task<DocumentAggregateRoot?> GetEmptyDraftByUserAsync(UserId UserId);
}

public interface IDocumentRepository : IBonRepository<DocumentAggregateRoot,DocumentId> 
{
    
}