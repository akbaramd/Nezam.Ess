using Bonyan.User;
using Bonyan.UserManagement.Domain.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.Secretariat.Application.Documents;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Documents;

public class CreateOrGetDraftDocumentEndpoint : EndpointWithoutRequest<DocumentDto>
{
    private readonly IDocumentApplicationService _documentService;
    private readonly ICurrentUser _currentUser;

    public CreateOrGetDraftDocumentEndpoint(IDocumentApplicationService documentService, ICurrentUser currentUser)
    {
        _documentService = documentService;
        _currentUser = currentUser;
    }

    public override void Configure()
    {
        Get("/api/document/draft");

        Description(c =>
        {
            c.WithTags("Documents");
        });
        Roles("admin","employer","engineer");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = UserId.FromGuid(_currentUser.Id!.Value); // Assuming `GetUserId` fetches UserId from claims
        var documentDraft = await _documentService.CreateOrGetEmptyDraftAsync(userId);

        await SendOkAsync(documentDraft, ct);
    }
}