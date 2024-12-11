﻿    using Bonyan.Layer.Domain.Entities;
    using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
    using Nezam.Modular.ESS.Identity.Domain.User;

    namespace Nezam.Modular.ESS.Identity.Domain.Roles
    {
        public class RoleEntity : BonEntity
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