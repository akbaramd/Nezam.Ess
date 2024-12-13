using FastEndpoints;
using Microsoft.AspNetCore.Mvc;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.WebApi.UseCases.Users;

public class UpdateUserRequest
{
    [FromRoute]
    public Guid UserId { get; set; } // User identifier

    [FastEndpoints.FromBody]
    public UserProfileValue Profile { get; set; } = default!; // Profile info
}

public class UpdateUserResponse
{
    public Guid UserId { get; set; }
    public string Message { get; set; } = "User updated successfully.";
}

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _workManager;

    public UpdateUserEndpoint(IUserDomainService userDomainService, IUnitOfWorkManager workManager)
    {
        _userDomainService = userDomainService;
        _workManager = workManager;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/api/users/{UserId}");
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        // Validate input
        if (req.UserId == Guid.Empty)
        {
            AddError("Invalid UserId.");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        // Start Unit of Work
        using var uow = _workManager.Begin();

        // Retrieve user by ID
        var userResult = await _userDomainService.GetUserByIdAsync(UserId.NewId(req.UserId));

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
        await SendAsync(new UpdateUserResponse
        {
            UserId = req.UserId,
            Message = "User updated successfully."
        }, cancellation: ct);
    }
}