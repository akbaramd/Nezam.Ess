using Bonyan.UserManagement.Domain.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Users;

public class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
{
    private readonly IUserService userService;

    public DeleteUserEndpoint(IUserService userService)
    {
        this.userService = userService;
    }

    public override void Configure()
    {
        Delete("/api/user/{UserId}");

        Description(c =>
        {
            c.WithTags("Users");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        var result = await userService.DeleteUserAsync(UserId.FromGuid(req.UserId));
        if (!result)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendOkAsync(ct);
    }
}

public class DeleteUserRequest
{
    public Guid UserId { get; set; }
}

