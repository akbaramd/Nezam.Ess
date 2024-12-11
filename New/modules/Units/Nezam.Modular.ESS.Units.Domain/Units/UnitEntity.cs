using Bonyan.Layer.Domain.Entities;
using Nezam.Modular.ESS.Units.Domain.Shared.Members;
using Nezam.Modular.ESS.Units.Domain.Shared.Units;

namespace Nezam.Modular.ESS.Units.Domain.Units
{
    public class UnitEntity : BonEntity
    {
        public UnitId UnitId { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private readonly List<UnitMemberEntity> _unitMembers = new List<UnitMemberEntity>();
        public IReadOnlyList<UnitMemberEntity> UnitMembers => _unitMembers;

        private UnitEntity() { }

        public UnitEntity(UnitId unitId, string name, string description)
        {
            UnitId = unitId ?? throw new ArgumentNullException(nameof(unitId));
            SetName(name);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name cannot be empty or null.");
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void AddMember(UnitMemberEntity memberEntity, string role)
        {
            if (memberEntity == null)
                throw new ArgumentNullException(nameof(memberEntity));

            if (_unitMembers.Exists(x => x.MemberId == memberEntity.MemberId))
                throw new InvalidOperationException("This member is already part of the unit.");

            var unitMember = new UnitMemberEntity(UnitId, memberEntity.MemberId, role);
            _unitMembers.Add(unitMember);
        }

        public void RemoveMember(MemberId memberId)
        {
            var unitMember = _unitMembers.Find(x => x.MemberId == memberId);
            if (unitMember == null)
                throw new InvalidOperationException("Member not found in this unit.");

            _unitMembers.Remove(unitMember);
        }

        public override object GetKey()
        {
            return UnitId;
        }
    }
}