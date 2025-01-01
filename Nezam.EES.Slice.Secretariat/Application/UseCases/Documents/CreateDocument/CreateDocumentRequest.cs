namespace Nezam.EES.Slice.Secretariat.Application.UseCases.Documents.CreateDocument;

public record CreateDocumentRequest(
    string Title,
    string Content,
    int Type,
    Guid ReceiverParticipantId
);