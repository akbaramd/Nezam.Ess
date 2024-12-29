using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Payeh.SharedKernel.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;
using Nezam.EEs.Shared.Domain.Identity.User;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.DeleteUser;

public class DeleteUserEndpoint : EndpointWithoutRequest
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public DeleteUserEndpoint(IUserDomainService userDomainService, IUnitOfWorkManager unitOfWork)
    {
        _userDomainService = userDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Delete("/api/users/{id:guid}");
        AllowAnonymous(); // Adjust as needed for authentication.
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdGuid = Route<Guid>("id", isRequired: true);

        using var uow = _unitOfWork.Begin();
        try
        {
            
            var userId = UserId.NewId(userIdGuid);
            
            var existingUserResult = await _userDomainService.GetUserByIdAsync(userId);
            if (existingUserResult.IsFailure)
            {
                ThrowError("User not found.");
            }
            
            var deleteResult = await _userDomainService.DeleteAsync(existingUserResult.Data);

            if (deleteResult.IsFailure)
            {
                ThrowError(deleteResult.ErrorMessage);
                return;
            }

            await uow.CommitAsync(ct);

            // Provide a structured response.
            await SendAsync(new { Message = "User deleted successfully", UserId = userId.Value }, cancellation: ct);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync(ct);
            ThrowError($"Error while deleting user: {ex.Message}");
        }
    }
}