using System;
using Bonyan.Layer.Domain.Model;
using FastEndpoints;
using Nezam.Modular.ESS.Identity.Application.Roles;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.WebApi.Endpoints.Roles;

public class RolePaginatedEndpoint : Endpoint<RoleFilterDto,PaginatedResult<RoleDto>>
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


        var rolePagianted = await roleService.GetPaginatedResult(dto);
        await SendOkAsync(rolePagianted,ct);
    }

}
