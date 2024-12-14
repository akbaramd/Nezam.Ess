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

    public class UserNameUpdatedEvent : DomainEvent
    {
        public UserId UserId { get; }
        public string NewUserName { get; }

        public UserNameUpdatedEvent(UserId userId, string newUserName)
        {
            UserId = userId;
            NewUserName = newUserName;
        }
    }

    public class UserPasswordUpdatedEvent : DomainEvent
    {
        public UserId UserId { get; }

        public UserPasswordUpdatedEvent(UserId userId)
        {
            UserId = userId;
        }
    }

    public class UserEmailUpdatedEvent : DomainEvent
    {
        public UserId UserId { get; }
        public string? NewEmail { get; }

        public UserEmailUpdatedEvent(UserId userId, string? newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }
    }

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



  
}
