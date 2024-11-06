using System.Collections.Generic;
using Bonyan.Layer.Domain.Entities;
using Bonyan.Layer.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Roles
{
    public class RoleEntity : Entity<RoleId>
    {
        // EF Core requires a parameterless constructor
        protected RoleEntity() { }

        public RoleEntity(RoleId id, string name, string title)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        private readonly List<UserEntity> _users = new List<UserEntity>();

        // Exposing collection directly without AsReadOnly() for EF Core compatibility
        public IReadOnlyCollection<UserEntity> Users => _users;

        // Method to update the role title
        public void UpdateTitle(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Name { get; private set; }
        public string Title { get; private set; }
    }

    // Value object for RoleId
    public class RoleId : BusinessId<RoleId>
    {
    }
}