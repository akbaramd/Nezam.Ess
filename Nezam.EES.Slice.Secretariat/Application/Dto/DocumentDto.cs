using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;

namespace Nezam.EES.Slice.Secretariat.Application.Dto
{
    public class DocumentDto
    {
        public Guid DocumentId { get; set; }
        public string Title { get; set; } = string.Empty; // Default empty string to avoid nulls
        public string Content { get; set; } = string.Empty;
        public ParticipantDto? OwnerParticipant { get; set; }
        public ParticipantDto? ReceiverParticipant { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TrackingCode { get; set; } = string.Empty;
        public int LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
        public List<DocumentAttachmentDto> Attachments { get; set; } = new();
        public List<DocumentReferralDto> Referrals { get; set; } = new();

        public static DocumentDto FromEntity(DocumentAggregateRoot entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentDto
            {
                DocumentId = entity.DocumentId?.Value ?? Guid.Empty,
                Title = entity.Title ?? string.Empty,
                Content = entity.Content ?? string.Empty,
                OwnerParticipant = entity.OwnerParticipant != null
                    ? ParticipantDto.FromEntity(entity.OwnerParticipant)
                    : null,
                ReceiverParticipant = entity.ReceiverParticipant != null
                    ? ParticipantDto.FromEntity(entity.ReceiverParticipant)
                    : null,
                Type = entity.Type?.ToString() ?? string.Empty,
                Status = entity.Status?.ToString() ?? string.Empty,
                TrackingCode = entity.TrackingCode ?? string.Empty,
                LetterNumber = entity.LetterNumber,
                LetterDate = entity.LetterDate,
                Attachments = entity.Attachments?.Select(DocumentAttachmentDto.FromEntity).ToList() ?? new List<DocumentAttachmentDto>(),
                Referrals = entity.Referrals?.Select(DocumentReferralDto.FromEntity).ToList() ?? new List<DocumentReferralDto>()
            };
        }
    }


    public class DocumentAttachmentDto
    {
        public Guid DocumentAttachmentId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

        public static DocumentAttachmentDto FromEntity(DocumentAttachmentEntity? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentAttachmentDto
            {
                DocumentAttachmentId = entity.DocumentAttachmentId?.Value ?? Guid.Empty,
                FileName = entity.FileName ?? string.Empty,
                FileType = entity.FileType ?? string.Empty,
                FileSize = entity.FileSize,
                FilePath = entity.FilePath ?? string.Empty,
                UploadDate = entity.UploadDate
            };
        }
    }


    public class DocumentReferralDto
    {
        public Guid DocumentReferralId { get; set; }
        public Guid DocumentId { get; set; }
        public ParticipantDto? ReferrerParticipant { get; set; }
        public ParticipantDto? ReceiverParticipant { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime ReferralDate { get; set; }
        public DateTime? ViewedDate { get; set; }
        public DateTime? RespondedDate { get; set; }
        public string? ResponseContent { get; set; }
        public Guid? ParentReferralId { get; set; }

        public static DocumentReferralDto FromEntity(DocumentReferralEntity? entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DocumentReferralDto
            {
                DocumentReferralId = entity.DocumentReferralId?.Value ?? Guid.Empty,
                DocumentId = entity.DocumentId?.Value ?? Guid.Empty,
                ReferrerParticipant = entity.ReferrerParticipant != null
                    ? ParticipantDto.FromEntity(entity.ReferrerParticipant)
                    : null,
                ReceiverParticipant = entity.ReceiverParticipant != null
                    ? ParticipantDto.FromEntity(entity.ReceiverParticipant)
                    : null,
                Status = entity.Status?.ToString() ?? string.Empty,
                ReferralDate = entity.ReferralDate,
                ViewedDate = entity.ViewedDate,
                RespondedDate = entity.RespondedDate,
                ResponseContent = entity.ResponseContent ?? string.Empty,
                ParentReferralId = entity.ParentReferralId?.Value
            };
        }
    }



    public class ParticipantDto
    {
        public Guid ParticipantId { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public static ParticipantDto FromEntity(Participant? entity)
        {
            if (entity == null) return null;

            return new ParticipantDto
            {
                ParticipantId = entity.ParticipantId?.Value ?? Guid.Empty,
                UserId = entity.UserId?.Value,
                Name = entity.Name ?? string.Empty
            };
        }
    }

}
