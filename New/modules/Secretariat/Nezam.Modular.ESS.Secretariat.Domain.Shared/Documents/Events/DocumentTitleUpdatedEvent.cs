using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentTitleUpdatedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentTitleUpdatedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}