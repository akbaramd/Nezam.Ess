using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Service.Identity.Domains.Roles
    {
        public class RoleEntity : Entity
        {
            protected RoleEntity() { }
            public RoleEntity(RoleId roleId, string title)
            {
                RoleId = roleId;
                Title = title;
            }

            public RoleId RoleId { get; set; }
            public string Title { get; set; }

            public ICollection<UserEntity> Users { get; private set; }
            // Method to add permissions (if relevant)

            public override object GetKey()
            {
                return RoleId;
            }
        }
    }