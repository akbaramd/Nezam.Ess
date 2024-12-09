﻿using Bonyan.User;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.Secretariat.Application.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.ValueObjects;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Documents;

public class UpdateDocumentEndpoint : Endpoint<DocumentUpdateDto, DocumentDto>
{
    private readonly IDocumentApplicationService _documentService;
    private readonly IBonCurrentUser _currentUser;

    public UpdateDocumentEndpoint(IDocumentApplicationService documentService, IBonCurrentUser currentUser)
    {
        _documentService = documentService;
        _currentUser = currentUser;
    }

    public override void Configure()
    {
        Put("/api/document/{DocumentId:guid}");

        Description(c =>
        {
            c.WithTags("Documents");
        });
        
        Roles("admin","employer","engineer");
    }

    public override async Task HandleAsync(DocumentUpdateDto req, CancellationToken ct)
    {
        // استخراج DocumentId از پارامترهای مسیر
        var documentId = DocumentId.NewId(Route<Guid>("DocumentId"));
        
        // فراخوانی متد به‌روزرسانی در سرویس
        var updatedDocument = await _documentService.UpdateAsync(documentId, req,BonUserId.NewId(_currentUser.Id!.Value!));

        if (updatedDocument == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(updatedDocument, ct);
    }
}