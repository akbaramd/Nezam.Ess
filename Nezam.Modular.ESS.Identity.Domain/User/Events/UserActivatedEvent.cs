using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class UserActivatedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public DateTime Timestamp { get; }

    public UserActivatedEvent(BonUserId userId)
    {
        UserId = userId;
        Timestamp = DateTime.UtcNow;
    }
}