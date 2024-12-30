using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Documents.Enumerations;
using Nezam.EES.Slice.Secretariat.Domains.Documents.ValueObjects;
using Nezam.EES.Slice.Secretariat.Domains.Participant;

namespace Nezam.EES.Slice.Secretariat.Application.Dto;

public class DocumentDto
{
    public DocumentId DocumentId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string TrackingCode { get; set; }
    public int LetterNumber { get; set; }
    public DateTime LetterDate { get; set; }
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
    public ParticipantDto Owner { get; set; }
    public ParticipantDto Receiver { get; set; }
    public List<DocumentAttachmentDto> Attachments { get; set; } = new();

    // Method to map from Document entity
    public static DocumentDto FromEntity(DocumentAggregateRoot document)
    {
        return new DocumentDto
        {
            DocumentId = document.DocumentId,
            Title = document.Title,
            TrackingCode = document.TrackingCode,
            LetterNumber = document.LetterNumber,
            LetterDate = document.LetterDate,
            Content = document.Content,
            Type = document.Type,
            Status = document.Status,
            Owner = ParticipantDto.FromEntity(document.OwnerParticipant),
            Receiver = ParticipantDto.FromEntity(document.ReceiverParticipant),
            Attachments = document.Attachments.Select(DocumentAttachmentDto.FromEntity).ToList()
        };
    }
}

public class DocumentAttachmentDto
{
    public string FileName { get; set; }
    public string FileType { get; set; }
    public long FileSize { get; set; }
    public string FilePath { get; set; }

    // Method to map from DocumentAttachmentEntity
    public static DocumentAttachmentDto FromEntity(DocumentAttachmentEntity attachment)
    {
        return new DocumentAttachmentDto
        {
            FileName = attachment.FileName,
            FileType = attachment.FileType,
            FileSize = attachment.FileSize,
            FilePath = attachment.FilePath
        };
    }
}

public class ParticipantDto
{
    public ParticipantId Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    // Method to map from Participant entity
    public static ParticipantDto FromEntity(Participant participant)
    {
        return (participant == null)?null: new ParticipantDto
        {
            Id = participant.ParticipantId,
            Name = participant.Name,
        };
    }
}


public class DocumentReferralDto
{
    // Properties for the DTO using business IDs
    public DocumentReferralId DocumentReferralId { get; set; }
    public DocumentId DocumentId { get; set; }
    public ParticipantId ReferrerUserId { get; set; }
    public string ReferrerUserFullName { get; set; } // Full name of the referrer (can be mapped from Participant)
    public ParticipantId ReceiverUserId { get; set; }
    public string ReceiverUserFullName { get; set; } // Full name of the receiver (can be mapped from Participant)
    public ReferralStatus Status { get; set; }
    public DateTime ReferralDate { get; set; }
    public DateTime? ViewedDate { get; set; }
    public DateTime? RespondedDate { get; set; }
    public string ResponseContent { get; set; }
    public DocumentReferralId? ParentReferralId { get; set; }

    // Constructor to map the entity to the DTO
    public DocumentReferralDto(DocumentReferralEntity entity)
    {
        DocumentReferralId = entity.DocumentReferralId;
        DocumentId = entity.DocumentId;
        ReferrerUserId = entity.ReferrerUserId;
        ReceiverUserId = entity.ReceiverUserId;
        Status = entity.Status;
        ReferralDate = entity.ReferralDate;
        ViewedDate = entity.ViewedDate;
        RespondedDate = entity.RespondedDate;
        ResponseContent = entity.ResponseContent;
        ParentReferralId = entity.ParentReferralId;

        // Optionally, populate the full names for users if you have access to them
        // (You can either fetch them separately or include them in the entity via navigation properties)
        ReferrerUserFullName = entity.ReferrerUser?.Name ?? string.Empty;
        ReceiverUserFullName = entity.ReceiverUser?.Name ?? string.Empty;
    }

    // Static method to create the DTO from a DocumentReferralEntity
    public static DocumentReferralDto FromEntity(DocumentReferralEntity entity)
    {
        return new DocumentReferralDto(entity);
    }
}