using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events
{
    // Event raised when a document is created
    public class DocumentCreatedEvent : DomainEvent
    {
        public DocumentId DocumentId { get; }
        
        public DocumentCreatedEvent(DocumentId documentId)
        {
            DocumentId = documentId;
        }
    }

    // Event raised when a document content is updated

    // Event raised when a document is sent

    // Event raised when a document is archived

    // Event raised when a document title is updated

    // Event raised when a document type is changed

    // Event raised when a document is reverted to draft
}
