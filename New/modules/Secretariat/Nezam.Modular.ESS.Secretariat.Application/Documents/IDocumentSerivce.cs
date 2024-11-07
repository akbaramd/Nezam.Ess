using Bonyan.Layer.Application.Services;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public interface IDocumentApplicationService : IBonApplicationService
{
    Task<DocumentDto> CreateOrGetEmptyDraftAsync(BonUserId BonUserId);
    Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, BonUserId BonUserId);
    Task AddPrimaryRecipientAsync(DocumentId documentId, BonUserId receiverBonUserId, BonUserId currentBonUserId);
    Task AddAttachmentAsync(DocumentId documentId, string fileName, string fileType, long fileSize, string filePath, BonUserId BonUserId);
    Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, BonUserId BonUserId);
    Task<DocumentDto> ViewDocumentDetailsAsync(DocumentId documentId);
    Task SendDocumentAsync(DocumentId documentId, BonUserId senderId);
}