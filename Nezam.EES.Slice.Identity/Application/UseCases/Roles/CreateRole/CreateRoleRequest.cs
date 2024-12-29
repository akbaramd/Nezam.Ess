using Nezam.EEs.Shared.Domain.Identity.Roles;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.CreateRole;

public record CreateRoleRequest(RoleId Id, string Title);