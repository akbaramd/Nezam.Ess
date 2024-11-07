using Bonyan.Layer.Domain.Model;
using FastEndpoints;
using Nezam.Modular.ESS.IdEntity.Application.Employers;
using Nezam.Modular.ESS.IdEntity.Application.Employers.Dtos;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Employers;

public class EmployerPaginatedEndpoint : Endpoint<EmployerFilterDto,BonPaginatedResult<EmployerDto>>
{
    private readonly IEmployerService userService;

    public EmployerPaginatedEndpoint(IEmployerService userService)
    {
        this.userService = userService;
    }

    public override void Configure()
    {
        Get("/api/employer/paginate");

        Description(c=>{
            c.WithTags("Employers");
        });

        AllowAnonymous();
    }

    public override async Task HandleAsync(EmployerFilterDto dto,CancellationToken ct)
    {
        var userPagianted = await userService.GetBonPaginatedResult(dto);
        await SendOkAsync(userPagianted,ct);
    }

}
