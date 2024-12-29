using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents;

public class UserProfileUpdatedEvent : DomainEvent
{
    public UserId UserId { get; }
    public UserProfileValue? NewProfile { get; }

    public UserProfileUpdatedEvent(UserId userId, UserProfileValue? newProfile)
    {
        UserId = userId;
        NewProfile = newProfile;
    }
}