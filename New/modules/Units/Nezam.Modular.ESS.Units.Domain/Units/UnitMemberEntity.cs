using Bonyan.Layer.Domain.Entities;
using Nezam.Modular.ESS.Units.Domain.Shared.Members;
using Nezam.Modular.ESS.Units.Domain.Shared.Units;

namespace Nezam.Modular.ESS.Units.Domain.Units
{
    public class UnitMemberEntity : BonEntity
    {
        public UnitMemberId UnitMemberId { get; private set; }
        public UnitId UnitId { get; private set; }
        public MemberId MemberId { get; private set; }
        public string Role { get; private set; }

        private UnitMemberEntity() { }

        public UnitMemberEntity(UnitId unitId, MemberId memberId, string role)
        {
            UnitMemberId = UnitMemberId.NewId();
            UnitId = unitId ?? throw new ArgumentNullException(nameof(unitId));
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            SetRole(role);
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty.");
            Role = role;
        }

        public override object GetKey()
        {
            return UnitMemberId;
        }
    }
}