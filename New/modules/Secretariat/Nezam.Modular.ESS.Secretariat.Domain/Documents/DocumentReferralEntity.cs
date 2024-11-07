﻿using Bonyan.Layer.Domain.Entities;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.BonEnumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Domain.Documents
{
    public class DocumentReferralEntity : BonEntity<DocumentReferralId>
    {
        public DocumentId DocumentId { get; private set; }
        public BonUserId ReferrerBonUserId { get; private set; }
        public BonUserId ReceiverBonUserId { get; private set; }
        public ReferralStatus Status { get; private set; }
        public DateTime ReferralDate { get; private set; }
        public DateTime? ViewedDate { get; private set; }
        public DateTime? RespondedDate { get; private set; }
        public string ResponseContent { get; private set; }
        public DocumentReferralId? ParentReferralId { get; private set; } // Parent referral for hierarchy management

        // Updated constructor with validation
        public DocumentReferralEntity(DocumentId documentId, BonUserId referrerBonUserId, BonUserId receiverBonUserId, DocumentReferralId? parentReferralId = null)
        {
            Id = DocumentReferralId.CreateNew();
            DocumentId = documentId ?? throw new ArgumentNullException(nameof(documentId));
            ReferrerBonUserId = referrerBonUserId ?? throw new ArgumentNullException(nameof(referrerBonUserId));
            ReceiverBonUserId = receiverBonUserId ?? throw new ArgumentNullException(nameof(receiverBonUserId));
            Status = ReferralStatus.Pending;
            ReferralDate = DateTime.UtcNow;
            ParentReferralId = parentReferralId;
        }

        public void MarkAsViewed()
        {
            if (Status == ReferralStatus.Responded)
                throw new InvalidOperationException("Cannot mark a responded referral as viewed.");
            if (Status == ReferralStatus.Canceled)
                throw new InvalidOperationException("Cannot mark a canceled referral as viewed.");

            Status = ReferralStatus.Viewed;
            ViewedDate = DateTime.UtcNow;

            // Optional: Trigger a domain event for referral viewed
            // AddDomainEvent(new ReferralViewedEvent(this.Id));
        }

        public void Respond(string responseContent)
        {
            if (Status == ReferralStatus.Responded)
                throw new InvalidOperationException("Referral has already been responded to.");
            if (Status == ReferralStatus.Canceled)
                throw new InvalidOperationException("Cannot respond to a canceled referral.");
            if (string.IsNullOrWhiteSpace(responseContent))
                throw new ArgumentException("Response content cannot be empty.", nameof(responseContent));

            Status = ReferralStatus.Responded;
            RespondedDate = DateTime.UtcNow;
            ResponseContent = responseContent;

            // Optional: Trigger a domain event for referral responded
            // AddDomainEvent(new ReferralRespondedEvent(this.Id));
        }

        public void Cancel()
        {
            if (Status == ReferralStatus.Responded)
                throw new InvalidOperationException("Cannot cancel a referral that has been responded to.");
            if (Status == ReferralStatus.Canceled)
                throw new InvalidOperationException("Referral is already canceled.");

            Status = ReferralStatus.Canceled;

            // Optional: Trigger a domain event for referral canceled
            // AddDomainEvent(new ReferralCanceledEvent(this.Id));
        }

        public bool IsProcessed()
        {
            return Status == ReferralStatus.Responded;
        }
    }
}
