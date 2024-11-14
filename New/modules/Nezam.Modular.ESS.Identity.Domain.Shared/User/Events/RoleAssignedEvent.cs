using Bonyan.Layer.Domain.Events;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.User.Events;

public class RoleAssignedEvent : BonDomainEventBase
{
    public BonUserId UserId { get; }
    public RoleId RoleId { get; }
    public DateTime Timestamp { get; }

    public RoleAssignedEvent(BonUserId userId, RoleId roleName)
    {
        UserId = userId;
        RoleId = roleName;
        Timestamp = DateTime.UtcNow;
    }
}