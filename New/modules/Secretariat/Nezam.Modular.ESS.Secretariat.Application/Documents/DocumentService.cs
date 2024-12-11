using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Enumerations;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public class DocumentApplicationService : BonApplicationService, IDocumentApplicationService
{
    private IDocumentRepository DocumentRepository => LazyServiceProvider.LazyGetRequiredService<IDocumentRepository>();

    private IDocumentReadOnlyRepository DocumentReadOnlyRepository =>
        LazyServiceProvider.LazyGetRequiredService<IDocumentReadOnlyRepository>();

    private IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

    // 1. ایجاد یا برگرداندن نامه پیشنویس خالی
    public async Task<DocumentDto> CreateOrGetEmptyDraftAsync(UserId UserId)
    {
        // تلاش برای یافتن یک نامه پیشنویس خالی بدون گیرنده و پیوست
        var existingDraft = await DocumentReadOnlyRepository.GetEmptyDraftByUserAsync(UserId);
        if (existingDraft != null && existingDraft.Status == DocumentStatus.Draft && !existingDraft.Attachments.Any() &&
            !existingDraft.Referrals.Any())
        {
            return Mapper.Map<DocumentDto>(existingDraft);
        }

        // ایجاد نامه پیشنویس جدید در صورت عدم وجود نامه پیشنویس
        var newDraft = new DocumentAggregateRoot(
            title: string.Empty,
            content: string.Empty,
            senderUserId: UserId,
            type: DocumentType.Internal
        );

        await DocumentRepository.AddAsync(newDraft);
        return Mapper.Map<DocumentDto>(newDraft);
    }

    public async Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, UserId UserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.UpdateContent(dto.Content, UserId);
        document.UpdateTitle(dto.Title, UserId);
        var type = BonEnumeration.FromId<DocumentType>(dto.Type);

        if (type != null)
        {
            document.ChangeType(type, UserId);
        }

        await DocumentRepository.UpdateAsync(document);

        return Mapper.Map<DocumentDto>(document);
    }

    // 2. اضافه کردن گیرنده اصلی
    public async Task AddPrimaryRecipientAsync(DocumentId documentId, UserId receiverUserId, UserId currentUserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        // بررسی ارجاعات موجود برای این گیرنده خاص
        var existingReferral = document.Referrals
            .FirstOrDefault(r => r.ReceiverUserId == receiverUserId &&
                                 r.Status == ReferralStatus.Pending);
        // اضافه کردن گیرنده به عنوان ارجاع اصلی
        document.AddInitialReferral(receiverUserId, currentUserId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 3. اضافه کردن پیوست
    public async Task AddAttachmentAsync(DocumentId documentId, string fileName, string fileType, long fileSize,
        string filePath, UserId UserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.AddAttachment(fileName, fileType, fileSize, filePath, UserId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 4. حذف پیوست
    public async Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, UserId UserId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.RemoveAttachment(attachmentId, UserId);
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
    public async Task SendDocumentAsync(DocumentId documentId, UserId senderId)
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