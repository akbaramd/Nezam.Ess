using Nezam.EEs.Shared.Domain.Identity.Roles;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.UpdateRole;

public record UpdateRoleRequest(RoleId Id, string Title);