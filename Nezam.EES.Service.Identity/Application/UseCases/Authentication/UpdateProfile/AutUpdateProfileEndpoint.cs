using System.Security.Claims;
using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication;

public class AutUpdateProfileEndpoint : Endpoint<AuthUpdateProfileRequest, AuthUpdateUserResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _workManager;

    public AutUpdateProfileEndpoint(IUserDomainService userDomainService, IUnitOfWorkManager workManager)
    {
        _userDomainService = userDomainService;
        _workManager = workManager;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/api/auth/profile");
    }

    public override async Task HandleAsync(AuthUpdateProfileRequest req, CancellationToken ct)
    {
        var userIdClaim = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
        {
            AddError("User is not authenticated.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        
        // Start Unit of Work
        using var uow = _workManager.Begin();

        // Retrieve user by ID
        var userResult = await _userDomainService.GetUserByIdAsync(UserId.NewId(userId));

        if (userResult.IsFailure || userResult.Data == null)
        {
            AddError(userResult.ErrorMessage ?? "User not found.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Update user profile
        userResult.Data.UpdateProfile(req.Profile);

        // Save updates through domain service
        var updateResult = await _userDomainService.UpdateAsync(userResult.Data);

        if (!updateResult.IsSuccess)
        {
            AddError(updateResult.ErrorMessage ?? "Failed to update user.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Commit the transaction
        await uow.CommitAsync();

        // Send successful response
        await SendAsync(new AuthUpdateUserResponse
        {
            UserId = userId,
            Message = "User updated successfully."
        }, cancellation: ct);
    }
}