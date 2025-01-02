using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserRoleUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public string[] Roles { get; }

    public UserRoleUpdatedEvent(UserId userId, string[] roles)
    {
        UserId = userId;
        Roles = roles;
    }
}