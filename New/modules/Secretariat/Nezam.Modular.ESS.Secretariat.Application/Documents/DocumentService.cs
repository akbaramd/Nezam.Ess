using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Repositories;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Secretariat.Application.Documents;

public class DocumentApplicationService : ApplicationService, IDocumentApplicationService
{
    private IDocumentRepository DocumentRepository => LazyServiceProvider.LazyGetRequiredService<IDocumentRepository>();

    private IDocumentReadOnlyRepository DocumentReadOnlyRepository =>
        LazyServiceProvider.LazyGetRequiredService<IDocumentReadOnlyRepository>();

    private IUserRepository UserRepository => LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

    // 1. ایجاد یا برگرداندن نامه پیشنویس خالی
    public async Task<DocumentDto> CreateOrGetEmptyDraftAsync(UserId userId)
    {
        // تلاش برای یافتن یک نامه پیشنویس خالی بدون گیرنده و پیوست
        var existingDraft = await DocumentReadOnlyRepository.GetEmptyDraftByUserAsync(userId);
        if (existingDraft != null && existingDraft.Status == DocumentStatus.Draft && !existingDraft.Attachments.Any() &&
            !existingDraft.Referrals.Any())
        {
            return Mapper.Map<DocumentDto>(existingDraft);
        }

        // ایجاد نامه پیشنویس جدید در صورت عدم وجود نامه پیشنویس
        var newDraft = new DocumentAggregateRoot(
            title: string.Empty,
            content: string.Empty,
            senderUserId: userId,
            type: DocumentType.Internal
        );

        await DocumentRepository.AddAsync(newDraft);
        return Mapper.Map<DocumentDto>(newDraft);
    }

    public async Task<DocumentDto> UpdateAsync(DocumentId documentId, DocumentUpdateDto dto, UserId userId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.UpdateContent(dto.Content, userId);
        document.UpdateTitle(dto.Title, userId);
        var type = Enumeration.FromId<DocumentType>(dto.Type);

        if (type != null)
        {
            document.ChangeType(type, userId);
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
        string filePath, UserId userId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.AddAttachment(fileName, fileType, fileSize, filePath, userId);
        await DocumentRepository.UpdateAsync(document);
    }

    // 4. حذف پیوست
    public async Task RemoveAttachmentAsync(DocumentId documentId, DocumentAttachmentId attachmentId, UserId userId)
    {
        var document = await DocumentReadOnlyRepository.GetByIdAsync(documentId);
        if (document == null)
            throw new Exception("documentId");

        document.RemoveAttachment(attachmentId, userId);
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