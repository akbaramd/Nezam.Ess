using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.Domain.DomainEvents;

namespace Nezam.EEs.Shared.Domain.Identity.User.DomainEvents
{
    public class UserCreatedEvent : DomainEvent
    {
        public UserId UserId { get; }
        public string UserName { get; }
        public string? Email { get; }
        public UserProfileValue Profile { get; }

        public UserCreatedEvent(UserId userId, string userName, string? email, UserProfileValue profile)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Profile = profile;
        }
    }
}
