using Bonyan.Layer.Domain.Events;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.User.Events
{
    public class UserCreatedEvent : BonDomainEventBase
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

    public class UserNameUpdatedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }
        public string NewUserName { get; }

        public UserNameUpdatedEvent(UserId userId, string newUserName)
        {
            UserId = userId;
            NewUserName = newUserName;
        }
    }

    public class UserPasswordUpdatedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }

        public UserPasswordUpdatedEvent(UserId userId)
        {
            UserId = userId;
        }
    }

    public class UserEmailUpdatedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }
        public string? NewEmail { get; }

        public UserEmailUpdatedEvent(UserId userId, string? newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }
    }

    public class UserProfileUpdatedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }
        public UserProfileValue NewProfile { get; }

        public UserProfileUpdatedEvent(UserId userId, UserProfileValue newProfile)
        {
            UserId = userId;
            NewProfile = newProfile;
        }
    }

    public class UserRoleAddedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }
        public RoleId RoleId { get; }
        public string RoleTitle { get; }

        public UserRoleAddedEvent(UserId userId, RoleId roleId, string roleTitle)
        {
            UserId = userId;
            RoleId = roleId;
            RoleTitle = roleTitle;
        }
    }

    public class UserRoleRemovedEvent : BonDomainEventBase
    {
        public UserId UserId { get; }
        public RoleId RoleId { get; }
        public string RoleTitle { get; }

        public UserRoleRemovedEvent(UserId userId, RoleId roleId, string roleTitle)
        {
            UserId = userId;
            RoleId = roleId;
            RoleTitle = roleTitle;
        }
    }
}
