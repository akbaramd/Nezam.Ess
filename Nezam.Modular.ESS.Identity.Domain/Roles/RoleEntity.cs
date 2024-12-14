using Nezam.Modular.ESS.Identity.Domain.User;
    using Payeh.SharedKernel.Domain;

    namespace Nezam.Modular.ESS.Identity.Domain.Roles
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