using Bonyan.Layer.Application.Services;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public interface IDocumentApplicationService : IBonApplicationService
{
    Task<DocumentDto> CreateOrGetEmptyDraftAsync(UserId UserId);
    Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, UserId UserId);
    Task AddPrimaryRecipientAsync(DocumentId documentId, UserId receiverUserId, UserId currentUserId);
    Task AddAttachmentAsync(DocumentId documentId, string fileName, string fileType, long fileSize, string filePath, UserId UserId);
    Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, UserId UserId);
    Task<DocumentDto> ViewDocumentDetailsAsync(DocumentId documentId);
    Task SendDocumentAsync(DocumentId documentId, UserId senderId);
}