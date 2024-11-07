using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class UserDeactivatedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public DateTime Timestamp { get; }

    public UserDeactivatedEvent(BonUserId userId)
    {
        UserId = userId;
        Timestamp = DateTime.UtcNow;
    }
}