using FastEndpoints;
using Nezam.EES.Service.Identity.Application.UseCases.Users.CreateUser;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.UpdateUser;

public class UpdateUserEndpoint : Endpoint<UpdateUserRecord>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public UpdateUserEndpoint(IUserDomainService userDomainService, IUnitOfWorkManager unitOfWork)
    {
        _userDomainService = userDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Put("/api/users/{id:guid}");
        AllowAnonymous(); // Adjust as needed
    }

    public override async Task HandleAsync(UpdateUserRecord req, CancellationToken ct)
    {
       
        var userIdGuid = Route<Guid>("id",true);

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
            existingUser.UpdateProfile(new UserProfileValue(req.FirstName,req.LastName));
            existingUser.SetEmail(new UserEmailValue(req.Email));

            var updateResult = await _userDomainService.UpdateAsync(existingUser);

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
            ThrowError($"Error while updating user: {ex.Message}");
        }
    }
}

