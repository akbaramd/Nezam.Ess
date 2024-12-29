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
    public DocumentType Type { get; set; }
    public DocumentStatus Status { get; set; }
    public ParticipantDto Owner { get; set; }
    public ParticipantDto Receiver { get; set; }

    // Method to map from Document entity
    public static DocumentDto FromEntity(DocumentAggregateRoot document)
    {
        return new DocumentDto
        {
            DocumentId = document.DocumentId,
            Title = document.Title,
            Content = document.Content,
            Type = document.Type,
            Status = document.Status,
            Owner = ParticipantDto.FromEntity(document.OwnerParticipant),
            Receiver = ParticipantDto.FromEntity(document.ReceiverParticipant)
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
        return new ParticipantDto
        {
            Id = participant.ParticipantId,
            Name = participant.Name,
        };
    }
}