using Bonyan.Layer.Application.Services;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public interface IDocumentApplicationService : IApplicationService
{
    Task<DocumentDto> CreateOrGetEmptyDraftAsync(UserId userId);
    Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, UserId userId);
    Task AddPrimaryRecipientAsync(DocumentId documentId, UserId receiverUserId, UserId currentUserId);
    Task AddAttachmentAsync(DocumentId documentId, string fileName, string fileType, long fileSize, string filePath, UserId userId);
    Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, UserId userId);
    Task<DocumentDto> ViewDocumentDetailsAsync(DocumentId documentId);
    Task SendDocumentAsync(DocumentId documentId, UserId senderId);
}