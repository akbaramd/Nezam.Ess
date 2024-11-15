using Bonyan.Layer.Domain.Repository.Abstractions;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Roles;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Roles;

public class RolePaginatedEndpoint : Endpoint<RoleFilterDto,BonPaginatedResult<RoleDto>>
{
    private readonly IRoleService roleService;

    public RolePaginatedEndpoint(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    public override void Configure()
    {
        Get("/api/role/paginate");

        Description(c=>{
            c.WithTags("Roles");
        });

       
        
        AllowAnonymous();
    }

    public override async Task HandleAsync(RoleFilterDto dto,CancellationToken ct)
    {


        var rolePagianted = await roleService.GetBonPaginatedResult(dto);
        await SendOkAsync(rolePagianted,ct);
    }

}
