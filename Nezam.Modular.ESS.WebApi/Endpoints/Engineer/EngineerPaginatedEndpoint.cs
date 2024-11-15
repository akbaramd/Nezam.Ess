using Bonyan.Layer.Domain.Repository.Abstractions;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Engineers;
using Nezam.Modular.ESS.Identity.Application.Engineers.Dtos;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Engineer;

public class EngineerPaginatedEndpoint : Endpoint<EngineerFilterDto,BonPaginatedResult<EngineerDtoWithDetails>>
{
    private readonly IEngineerService userService;

    public EngineerPaginatedEndpoint(IEngineerService userService)
    {
        this.userService = userService;
    }

    public override void Configure()
    {
        Get("/api/engineer/paginate");

        Description(c=>{
            c.WithTags("Engineers");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(EngineerFilterDto dto,CancellationToken ct)
    {
        var userPagianted = await userService.GetBonPaginatedResult(dto);
        await SendOkAsync(userPagianted,ct);
    }

}
