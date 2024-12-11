using Bonyan.User;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.Secretariat.Application.Documents;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Documents;

public class CreateOrGetDraftDocumentEndpoint : EndpointWithoutRequest<DocumentDto>
{
    private readonly IDocumentApplicationService _documentService;
    private readonly IBonCurrentUser _currentUser;

    public CreateOrGetDraftDocumentEndpoint(IDocumentApplicationService documentService, IBonCurrentUser currentUser)
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
        var bonUserId = BonUserId.NewId(_currentUser.Id!.Value); // Assuming `GetBonUserId` fetches BonUserId from claims
        var documentDraft = await _documentService.CreateOrGetEmptyDraftAsync(bonUserId);

        await SendOkAsync(documentDraft, ct);
    }
}