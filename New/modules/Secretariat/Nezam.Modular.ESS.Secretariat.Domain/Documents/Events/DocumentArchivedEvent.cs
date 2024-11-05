using Bonyan.Layer.Domain.Events;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentArchivedEvent : DomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentArchivedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}