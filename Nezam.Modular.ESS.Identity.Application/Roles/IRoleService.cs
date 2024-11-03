using System;
using Bonyan.Layer.Application.Services;
using Bonyan.Layer.Domain.Model;
using Nezam.Modular.ESS.Identity.Application.Roles.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Roles;

public interface IRoleService : IApplicationService
{
    Task<PaginatedResult<RoleDto>> GetPaginatedResult(RoleFilterDto filterDto);
}
