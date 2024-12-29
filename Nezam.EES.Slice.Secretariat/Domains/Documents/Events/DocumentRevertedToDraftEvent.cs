using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentRevertedToDraftEvent : DomainEvent
{
    public DocumentId DocumentId { get; }

    public DocumentRevertedToDraftEvent(DocumentId documentId)
    {
        DocumentId = documentId;
    }
}