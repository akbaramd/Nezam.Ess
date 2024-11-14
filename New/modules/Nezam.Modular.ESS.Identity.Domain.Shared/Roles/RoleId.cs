using Bonyan.Layer.Domain.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.Roles
{
    public class RoleId : BonValueObject
    {
        public RoleId(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; private set; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}