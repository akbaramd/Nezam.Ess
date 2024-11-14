using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.Engineer;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User.Events;

public class EngineerRemovedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public EngineerId EngineerId { get; }
    public DateTime Timestamp { get; }

    public EngineerRemovedEvent(BonUserId userId, EngineerId engineerId)
    {
        UserId = userId;
        EngineerId = engineerId;
        Timestamp = DateTime.UtcNow;
    }
}