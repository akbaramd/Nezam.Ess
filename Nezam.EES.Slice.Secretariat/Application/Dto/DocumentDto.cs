using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;

namespace Nezam.EES.Slice.Secretariat.Application.Dto
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; } // Using raw GUID for external compatibility
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid OwnerParticipantId { get; set; } // Using raw GUID for external compatibility
        public string OwnerParticipantName { get; set; }
        public Guid ReceiverParticipantId { get; set; } // Using raw GUID for external compatibility
        public string ReceiverParticipantName { get; set; }
        public string Type { get; set; } // Enum as string for external compatibility
        public string Status { get; set; } // Enum as string for external compatibility
        public string TrackingCode { get; set; }
        public int LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
        public List<DocumentAttachmentDto> Attachments { get; set; } = new();
        public List<DocumentReferralDto> Referrals { get; set; } = new();

        public static DocumentDto FromEntity(DocumentAggregateRoot entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentDto
            {
                DocumentId = entity.DocumentId.Value,
                Title = entity.Title,
                Content = entity.Content,
                OwnerParticipantId = entity.OwnerParticipantId.Value,
                OwnerParticipantName = entity.OwnerParticipant?.Name,
                ReceiverParticipantId = entity.ReceiverParticipantId.Value,
                ReceiverParticipantName = entity.ReceiverParticipant?.Name,
                Type = entity.Type.ToString(),
                Status = entity.Status.ToString(),
                TrackingCode = entity.TrackingCode,
                LetterNumber = entity.LetterNumber,
                LetterDate = entity.LetterDate,
                Attachments = entity.Attachments.Select(DocumentAttachmentDto.FromEntity).ToList(),
                Referrals = entity.Referrals.Select(DocumentReferralDto.FromEntity).ToList()
            };
        }
    }

    public class DocumentAttachmentDto
    {
        public Guid DocumentAttachmentId { get; set; } // Using raw GUID for external compatibility
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }

        public static DocumentAttachmentDto FromEntity(DocumentAttachmentEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentAttachmentDto
            {
                DocumentAttachmentId = entity.DocumentAttachmentId.Value,
                FileName = entity.FileName,
                FileType = entity.FileType,
                FileSize = entity.FileSize,
                FilePath = entity.FilePath,
                UploadDate = entity.UploadDate
            };
        }
    }

    public class DocumentReferralDto
    {
        public Guid DocumentReferralId { get; set; } // Using raw GUID for external compatibility
        public Guid DocumentId { get; set; } // Using raw GUID for external compatibility
        public Guid ReferrerUserId { get; set; } // Using raw GUID for external compatibility
        public string ReferrerUserName { get; set; }
        public Guid ReceiverUserId { get; set; } // Using raw GUID for external compatibility
        public string ReceiverUserName { get; set; }
        public string Status { get; set; } // Enum as string for external compatibility
        public DateTime ReferralDate { get; set; }
        public DateTime? ViewedDate { get; set; }
        public DateTime? RespondedDate { get; set; }
        public string? ResponseContent { get; set; }
        public Guid? ParentReferralId { get; set; } // Using raw GUID for external compatibility

        public static DocumentReferralDto FromEntity(DocumentReferralEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentReferralDto
            {
                DocumentReferralId = entity.DocumentReferralId.Value,
                DocumentId = entity.DocumentId.Value,
                ReferrerUserId = entity.ReferrerUserId.Value,
                ReferrerUserName = entity.ReferrerUser?.Name,
                ReceiverUserId = entity.ReceiverUserId.Value,
                ReceiverUserName = entity.ReceiverUser?.Name,
                Status = entity.Status.ToString(),
                ReferralDate = entity.ReferralDate,
                ViewedDate = entity.ViewedDate,
                RespondedDate = entity.RespondedDate,
                ResponseContent = entity.ResponseContent,
                ParentReferralId = entity.ParentReferralId?.Value
            };
        }
    }

    public class ParticipantDto
    {
        public Guid ParticipantId { get; set; } // Using raw GUID for external compatibility
        public Guid? UserId { get; set; } // Using raw GUID for external compatibility
        public string Name { get; set; }

        public static ParticipantDto FromEntity(Participant entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new ParticipantDto
            {
                ParticipantId = entity.ParticipantId.Value,
                UserId = entity.UserId?.Value,
                Name = entity.Name
            };
        }
    }
}
