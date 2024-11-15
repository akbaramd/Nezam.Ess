using Bonyan.Layer.Domain.Entities;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

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
