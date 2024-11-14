using Bonyan.Layer.Domain.Aggregates;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

namespace Nezam.Modular.ESS.Identity.Domain.Roles
{
    public class RoleEntity : BonAggregateRoot<RoleId>
    {
        // EF Core نیاز به سازنده بدون پارامتر دارد
        protected RoleEntity() { }

        public RoleEntity(RoleId id, string title)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; private set; }

        // متد به‌روزرسانی عنوان نقش
        public void UpdateTitle(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }
    }
}