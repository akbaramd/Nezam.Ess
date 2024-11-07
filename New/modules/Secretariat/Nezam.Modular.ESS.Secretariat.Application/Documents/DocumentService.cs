using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.BonEnumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public class DocumentApplicationService : BonApplicationService, IDocumentApplicationService
{
    private IDocumentRepository DocumentRepository => LazyServiceProvider.LazyGetRequiredService<IDocumentRepository>();

    private IDocumentReadOnlyRepository DocumentReadOnlyRepository =>
        LazyServiceProvider.LazyGetRequiredService<IDocumentReadOnlyRepository>();

    private IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

    // 1. ایجاد یا برگرداندن نامه پیشنویس خالی
    public async Task<DocumentDto> CreateOrGetEmptyDraftAsync(BonUserId BonUserId)
    {
        // تلاش برای یافتن یک نامه پیشنویس خالی بدون گیرنده و پیوست
        var existingDraft = await DocumentReadOnlyRepository.GetEmptyDraftByUserAsync(BonUserId);
        if (existingDraft != null && existingDraft.Status == DocumentStatus.Draft && !existingDraft.Attachments.Any() &&
            !existingDraft.Referrals.Any())
        {
            return Mapper.Map<DocumentDto>(existingDraft);
        }

        // ایجاد نامه پیشنویس جدید در صورت عدم وجود نامه پیشنویس
        var newDraft = new DocumentAggregateRoot(
            title: string.Empty,
            content: string.Empty,
            senderBonUserId: BonUserId,
            type: DocumentType.Internal
        );

        await DocumentRepository.AddAsync(newDraft);
        return Mapper.Map<DocumentDto>(newDraft);
    }

    public async Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, BonUserId BonUserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.UpdateContent(dto.Content, BonUserId);
        document.UpdateTitle(dto.Title, BonUserId);
        var type = BonEnumeration.FromId<DocumentType>(dto.Type);

        if (type != null)
        {
            document.ChangeType(type, BonUserId);
        }

        await DocumentRepository.UpdateAsync(document);

        return Mapper.Map<DocumentDto>(document);
    }

    // 2. اضافه کردن گیرنده اصلی
    public async Task AddPrimaryRecipientAsync(DocumentId documentId, BonUserId receiverBonUserId, BonUserId currentBonUserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        // بررسی ارجاعات موجود برای این گیرنده خاص
        var existingReferral = document.Referrals
            .FirstOrDefault(r => r.ReceiverBonUserId == receiverBonUserId &&
                                 r.Status == ReferralStatus.Pending);
        // اضافه کردن گیرنده به عنوان ارجاع اصلی
        document.AddInitialReferral(receiverBonUserId, currentBonUserId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 3. اضافه کردن پیوست
    public async Task AddAttachmentAsync(DocumentId documentId, string fileName, string fileType, long fileSize,
        string filePath, BonUserId BonUserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.AddAttachment(fileName, fileType, fileSize, filePath, BonUserId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 4. حذف پیوست
    public async Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, BonUserId BonUserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.RemoveAttachment(attachmentId, BonUserId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 5. مشاهده جزئیات نامه
    public async Task<DocumentDto> ViewDocumentDetailsAsync(DocumentId documentId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        return Mapper.Map<DocumentDto>(document);
    }

    // 6. ارسال نامه
    public async Task SendDocumentAsync(DocumentId documentId, BonUserId senderId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        // بررسی وجود گیرنده و وضعیت پیشنویس بودن نامه
        if (!document.Referrals.Any())
            throw new InvalidOperationException("Cannot send a document without recipients.");
        if (document.Status != DocumentStatus.Draft)
            throw new InvalidOperationException("Only draft documents can be sent.");

        // تغییر وضعیت نامه به "Published"
        document.Publish(senderId);
        await DocumentRepository.UpdateAsync(document);
    }
}