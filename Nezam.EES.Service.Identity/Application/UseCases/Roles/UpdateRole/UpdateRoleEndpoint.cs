using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.UpdateRole;

public class UpdateRoleEndpoint : Endpoint<UpdateRoleRequest, UpdateRoleResponse>
{
    private readonly IRoleDomainService _roleDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public UpdateRoleEndpoint(IRoleDomainService roleDomainService, IUnitOfWorkManager unitOfWork)
    {
        _roleDomainService = roleDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Put("/roles/update");
        AllowAnonymous(); // تغییر دهید در صورت نیاز
    }

    public override async Task HandleAsync(UpdateRoleRequest req, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(req.Title))
        {
            ThrowError("Title cannot be empty.");
        }

        using var uow = _unitOfWork.Begin();
        try
        {

            var roleResult = await _roleDomainService.GetRoleByIdAsync(req.Id);
            if (roleResult.IsFailure)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }
            
            roleResult.Data.SetTitle(req.Title);
            // بررسی وجود Role و به‌روزرسانی آن
            var createResult = await _roleDomainService.UpdateRoleAsync(roleResult.Data);

            if (roleResult.IsFailure)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            // ذخیره تغییرات
            await uow.CommitAsync(ct);

            await SendAsync(new UpdateRoleResponse(req.Id, "Role updated successfully."), cancellation: ct);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync(ct);
            ThrowError($"Error while updating role: {ex.Message}");
        }
    }
}