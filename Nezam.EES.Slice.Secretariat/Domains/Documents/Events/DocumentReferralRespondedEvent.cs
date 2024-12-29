using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EES.Slice.Secretariat.Domains.Documents.Events;

public class DocumentReferralRespondedEvent : DomainEvent
{
    public DocumentReferralRespondedEvent(DocumentId id, DocumentReferralId referralId)
    {
        
    }
}