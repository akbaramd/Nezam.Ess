using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Payeh.SharedKernel.Domain;

namespace Nezam.EES.Service.Identity.Domains.Roles
{
    public class RoleEntity : Entity
    {
        // Protected constructor for EF Core
        protected RoleEntity() 
        {
        }

        public RoleEntity(RoleId roleId, string title) : this()
        {
            RoleId = roleId;
            SetTitle(title);
        }

        public RoleId RoleId { get; private set; }
        public string Title { get; private set; }

        public ICollection<UserEntity> Users { get; private set; }

        // Method to update title
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty", nameof(title));
            }

            if (title.Length > 50)
            {
                throw new ArgumentException("Title cannot exceed 50 characters", nameof(title));
            }

            Title = title;
        }

        // Override to provide unique key for this entity
        public override object GetKey()
        {
            return RoleId;
        }

    }
}