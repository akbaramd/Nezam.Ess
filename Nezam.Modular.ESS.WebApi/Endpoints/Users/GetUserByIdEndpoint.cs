using Bonyan.UserManagement.Domain.ValueObjects;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Users;

public class GetUserByIdEndpoint : Endpoint<GetUserByIdRequest, UserDtoWithDetail>
{
    private readonly IUserService userService;

    public GetUserByIdEndpoint(IUserService userService)
    {
        this.userService = userService;
    }

    public override void Configure()
    {
        Get("/api/user/{BonUserId}");

        Description(c =>
        {
            c.WithTags("Users");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserByIdRequest req, CancellationToken ct)
    {
        var user = await userService.GetUserByIdAsync(BonUserId.FromGuid(req.BonUserId));
        if (user == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendOkAsync(user, ct);
    }
}

public class GetUserByIdRequest
{
    public Guid BonUserId { get; set; }
}