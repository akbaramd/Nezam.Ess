using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Events;

public class DocumentReferralCreatedEvent : BonDomainEventBase
{
    public DocumentReferralCreatedEvent(DocumentId id, DocumentReferralId referralId, BonUserId receiverBonUserId,BonUserId referrerBonUserId)
    {
    }
}