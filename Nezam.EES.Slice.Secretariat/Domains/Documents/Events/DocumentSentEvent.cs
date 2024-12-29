using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentSentEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentSentEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}