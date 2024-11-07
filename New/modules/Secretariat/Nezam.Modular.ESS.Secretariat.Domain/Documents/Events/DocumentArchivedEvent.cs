using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents.Events;

public class DocumentArchivedEvent : BonDomainEventBase
{
    public DocumentId DocumentId { get; }

    public DocumentArchivedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}