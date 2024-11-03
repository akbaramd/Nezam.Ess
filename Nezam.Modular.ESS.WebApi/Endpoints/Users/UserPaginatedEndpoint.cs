using System;
using Bonyan.Layer.Domain.Model;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Users;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Users;

public class UserPaginatedEndpoint : Endpoint<UserFilterDto,PaginatedResult<UserDtoWithDetail>>
{
    private readonly IUserService userService;

    public UserPaginatedEndpoint(IUserService userService)
    {
        this.userService = userService;
    }

    public override void Configure()
    {
        Get("/api/user/paginate");

        Description(c=>{
            c.WithTags("Users");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(UserFilterDto dto,CancellationToken ct)
    {
        var userPagianted = await userService.GetPaginatedResult(dto);
        await SendOkAsync(userPagianted,ct);
    }

}
