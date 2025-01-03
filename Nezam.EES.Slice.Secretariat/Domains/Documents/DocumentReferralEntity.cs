    using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
    using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
    using Nezam.EES.Slice.Secretariat.Domains.Participant;
    using Payeh.SharedKernel.Domain;

    namespace Nezam.EES.Slice.Secretariat.Domains.Documents;

    public class DocumentReferralEntity : Entity
    {
        public DocumentReferralId DocumentReferralId { get; private set; }
        public DocumentId DocumentId { get; private set; }
        public ParticipantId ReferrerParticipantId { get; private set; } // Sender of the referral
        public Participant.Participant ReferrerParticipant { get; private set; }
        public ParticipantId ReceiverParticipantId { get; private set; } // Receiver of the referral
        public Participant.Participant ReceiverParticipant { get; private set; }
        public ReferralStatus Status { get; private set; }
        public DateTime ReferralDate { get; private set; }
        public DateTime? ViewedDate { get; private set; }
        public DateTime? RespondedDate { get; private set; }
        public string? ResponseContent { get; private set; }
        public DocumentReferralId? ParentReferralId { get; private set; } // Parent referral for hierarchy management

        // Constructor with validation
        public DocumentReferralEntity(
            DocumentId documentId,
            ParticipantId referrerUserId,
            ParticipantId receiverUserId,
            string? content = null,
            DocumentReferralId? parentReferralId = null)
        {
            DocumentReferralId = DocumentReferralId.NewId();
            DocumentId = documentId ?? throw new ArgumentNullException(nameof(documentId));
            ReferrerParticipantId = referrerUserId ?? throw new ArgumentNullException(nameof(referrerUserId));
            ReceiverParticipantId = receiverUserId ?? throw new ArgumentNullException(nameof(receiverUserId));
            ParentReferralId = parentReferralId;
            Status = ReferralStatus.Pending; // Default status
            ReferralDate = DateTime.UtcNow;
            ResponseContent = content;
        }

        protected DocumentReferralEntity() { }

        /// <summary>
        /// Marks the referral as viewed.
        /// </summary>
        public void MarkAsViewed()
        {
            if (Status == ReferralStatus.Canceled)
                throw new InvalidOperationException("Cannot mark a canceled referral as viewed.");

            if (Status == ReferralStatus.Responded)
                throw new InvalidOperationException("Cannot mark a responded referral as viewed.");

            Status = ReferralStatus.Viewed;
            ViewedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Responds to the referral.
        /// </summary>
        /// <param name="responseContent">The response content.</param>
        public void Respond()
        {
            Status = ReferralStatus.Responded;
            RespondedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels the referral.
        /// </summary>
        public void Cancel()
        {
            if (Status == ReferralStatus.Responded)
                throw new InvalidOperationException("Cannot cancel a referral that has already been responded to.");

            if (Status == ReferralStatus.Canceled)
                throw new InvalidOperationException("Referral is already canceled.");

            Status = ReferralStatus.Canceled;
        }

        /// <summary>
        /// Updates the referral's status based on provided value.
        /// </summary>
        /// <param name="newStatus">The new status to set.</param>
        /// <param name="responseContent">Optional response content, required for certain statuses.</param>

        public void UpdateStatus(ReferralStatus newStatus)
        {
            if (newStatus == ReferralStatus.Viewed)
            {
                MarkAsViewed();
            }
            else if (newStatus == ReferralStatus.Responded)
            {
              

                Respond();
            }
            else if (newStatus == ReferralStatus.Canceled)
            {
                Cancel();
            }
            else
            {
                Status = newStatus;
            }
        }

        /// <summary>
        /// Checks if the referral has been processed (responded).
        /// </summary>
        public bool IsProcessed()
        {
            return Status == ReferralStatus.Responded;
        }

        public override object GetKey()
        {
            return DocumentReferralId;
        }
    }
