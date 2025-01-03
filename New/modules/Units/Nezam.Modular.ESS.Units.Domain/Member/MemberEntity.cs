﻿using Nezam.Modular.ESS.Units.Domain.Shared.Members;
using Payeh.SharedKernel.Domain;

namespace Nezam.Modular.ESS.Units.Domain.Member
{
    public class MemberEntity : AggregateRoot
    {
        public MemberId MemberId { get; private set; }
        public UserId UserId { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }

        private MemberEntity() { }

        public MemberEntity(MemberId memberId, UserId userId, string? firstName, string? lastName)
        {
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            SetName(firstName, lastName);
        }

        public void SetName(string? firstName, string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void SyncWithUser(UserId userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            UserId = userId;
        }

        public override object GetKey()
        {
            return MemberId;
        }
    }
}