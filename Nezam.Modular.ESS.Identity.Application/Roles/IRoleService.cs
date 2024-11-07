using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.IdEntity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.IdEntity.Application.Roles;

public interface IRoleService : IBonApplicationService
{
    Task<BonPaginatedResult<RoleDto>> GetBonPaginatedResult(RoleFilterDto filterDto);
}
