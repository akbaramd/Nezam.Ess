using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentTypeChangedEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentTypeChangedEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}