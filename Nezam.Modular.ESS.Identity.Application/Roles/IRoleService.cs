using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Repository.Abstractions;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public interface IRoleService : IBonApplicationService
{
    Task<BonPaginatedResult<RoleDto>> GetBonPaginatedResult(RoleFilterDto filterDto);
}
