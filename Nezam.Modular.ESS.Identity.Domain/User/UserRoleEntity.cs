using Bonyan.Layer.Domain.Entities;
using Bonyan.UserManagement.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Employer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User.Events;

namespace Nezam.Modular.ESS.Identity.Domain.User
{
    public class UserRoleEntity : BonEntity
    {
        // Protected constructor for ORM
        protected UserRoleEntity() { }

        public UserRoleEntity(BonUserId bonUserId, RoleId roleId)
        {
            RoleId = roleId;
            UserId = bonUserId;
        }
        public RoleId RoleId { get; private set; }
        public BonUserId UserId { get; private set; }


        public override object[] GetKeys()
        {
            return [RoleId,UserId];
        }
    }
}
