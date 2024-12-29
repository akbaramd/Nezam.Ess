using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Users.CreateUser;

public class CreateUserEndpoint : Endpoint<CreateUserRecord>
{
    private readonly IUserDomainService _userDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public CreateUserEndpoint(IUserDomainService roleDomainService, IUnitOfWorkManager unitOfWork)
    {
        _userDomainService = roleDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Post("/api/users");
        
    }

    public override async Task HandleAsync(CreateUserRecord req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.UserName))
        {
            ThrowError("UserName cannot be empty.");
        }

        using var uow = _unitOfWork.Begin();
        try
        {

            var user = new UserEntity(UserId.NewId(), UserNameId.NewId(req.UserName),new UserPasswordValue(req.Password),new UserProfileValue(req.FirstName,req.LastName),new UserEmailValue(req.Email));
            var createResult = await _userDomainService.Create(user);
            
            if (createResult.IsFailure)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }
            // ذخیره تغییرات
            await uow.CommitAsync(ct);

            await SendOkAsync(ct);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync(ct);
            ThrowError($"Error while updating role: {ex.Message}");
        }
    }
}