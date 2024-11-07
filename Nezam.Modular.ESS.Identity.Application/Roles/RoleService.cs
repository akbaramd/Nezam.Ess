using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Abstractions;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Specs;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;

namespace Nezam.Modular.ESS.IdEntity.Application.Roles;

public class RoleService : BonApplicationService, IRoleService
{
    public IBonRepository<RoleEntity> RoleRepository => LazyServiceProvider.LazyGetRequiredService<IBonRepository<RoleEntity>>();

    public async Task<BonPaginatedResult<RoleDto>> GetBonPaginatedResult(RoleFilterDto filterDto)
    {
        var res = await RoleRepository.PaginatedAsync(new RolesFilterSpec(filterDto));
        return Mapper.Map<BonPaginatedResult<RoleEntity>,BonPaginatedResult<RoleDto>>(res);
    }
}
