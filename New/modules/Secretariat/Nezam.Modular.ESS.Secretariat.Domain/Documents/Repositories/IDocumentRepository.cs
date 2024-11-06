using Bonyan.Layer.Domain.Abstractions;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;

public interface IDocumentReadOnlyRepository : IReadOnlyRepository<DocumentAggregateRoot,DocumentId>
{
    Task<DocumentAggregateRoot?> GetEmptyDraftByUserAsync(UserId userId);
}

public interface IDocumentRepository : IRepository<DocumentAggregateRoot,DocumentId> 
{
    
}