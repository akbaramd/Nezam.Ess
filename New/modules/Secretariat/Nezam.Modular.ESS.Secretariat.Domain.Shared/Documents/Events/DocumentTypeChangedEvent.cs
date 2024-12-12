using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentTypeChangedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentTypeChangedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}