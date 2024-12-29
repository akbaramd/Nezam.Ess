using Consul;
using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.AssignRoles;

public class AssignRolesEndpoint : Endpoint<AssignRolesRecord>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public AssignRolesEndpoint(IUserDomainService userDomainService, IRoleDomainService roleDomainService, IUnitOfWorkManager unitOfWork)
    {
        _userDomainService = userDomainService;
        _roleDomainService = roleDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Put("/api/users/{id:guid}/assign-roles");
    }

    public override async Task HandleAsync(AssignRolesRecord req, CancellationToken ct)
    {
        var userIdGuid = Route<Guid>("id", true);
        var userId = UserId.NewId(userIdGuid);

        using var uow = _unitOfWork.Begin();
        try
        {
            var existingUserResult = await _userDomainService.GetUserByIdAsync(userId);

            if (existingUserResult.IsFailure)
            {
                ThrowError("User not found.");
            }

            var existingUser = existingUserResult.Data;

            // Retrieve roles by IDs from the request
            var roleIds = req.Roles.Select(RoleId.NewId).ToArray();
         

   

            var updateResult = await _userDomainService.AssignRoleAsync(existingUser,roleIds);

            if (updateResult.IsFailure)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            await uow.CommitAsync(ct);

            await SendOkAsync(ct);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync(ct);
            ThrowError($"Error while assigning roles to user: {ex.Message}");
        }
    }
}

public record AssignRolesRecord
{
    public List<string> Roles { get; set; } = new();
}
