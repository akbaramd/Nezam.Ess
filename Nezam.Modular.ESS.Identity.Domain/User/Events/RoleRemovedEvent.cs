using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;

namespace Nezam.Modular.ESS.IdEntity.Domain.User.Events;

public class RoleRemovedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public string RoleName { get; }
    public DateTime Timestamp { get; }

    public RoleRemovedEvent(BonUserId userId, string roleName)
    {
        UserId = userId;
        RoleName = roleName;
        Timestamp = DateTime.UtcNow;
    }
}