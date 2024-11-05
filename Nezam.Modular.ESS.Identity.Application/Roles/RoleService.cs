using System;
using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Abstractions;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Roles.Specs;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public class RoleService : ApplicationService, IRoleService
{
    public IRepository<RoleEntity> RoleRepository => LazyServiceProvider.LazyGetRequiredService<IRepository<RoleEntity>>();

    public async Task<PaginatedResult<RoleDto>> GetPaginatedResult(RoleFilterDto filterDto)
    {
        var res = await RoleRepository.PaginatedAsync(new RolesFilterSpec(filterDto));
        return Mapper.Map<PaginatedResult<RoleEntity>,PaginatedResult<RoleDto>>(res);
    }
}
