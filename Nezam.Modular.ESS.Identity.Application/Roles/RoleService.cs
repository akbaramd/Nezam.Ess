using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;
using Nezam.Modular.ESS.Identity.Application.Roles.Specs;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public class RoleService : BonApplicationService, IRoleService
{
    public IBonRepository<RoleEntity> RoleRepository => LazyServiceProvider.LazyGetRequiredService<IBonRepository<RoleEntity>>();

    public async Task<BonPaginatedResult<RoleDto>> GetBonPaginatedResult(RoleFilterDto filterDto)
    {
        var res = await RoleRepository.PaginatedAsync(new RolesFilterSpec(filterDto));
        return Mapper.Map<BonPaginatedResult<RoleEntity>,BonPaginatedResult<RoleDto>>(res);
    }
}
