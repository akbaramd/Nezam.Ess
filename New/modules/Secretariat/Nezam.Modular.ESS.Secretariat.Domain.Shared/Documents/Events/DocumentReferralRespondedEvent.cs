using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentReferralRespondedEvent : BonDomainEventBase
{
    public DocumentReferralRespondedEvent(DocumentId id, DocumentReferralId referralId)
    {
        
    }
}