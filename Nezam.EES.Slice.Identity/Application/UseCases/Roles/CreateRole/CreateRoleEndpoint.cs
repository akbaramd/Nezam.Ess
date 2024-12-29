using FastEndpoints;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.CreateRole;

public class CreateRoleEndpoint : Endpoint<CreateRoleRequest, CreateRoleResponse>
{
    private readonly IRoleDomainService _roleDomainService;
    private readonly IUnitOfWorkManager _unitOfWork;

    public CreateRoleEndpoint(IRoleDomainService roleDomainService, IUnitOfWorkManager unitOfWork)
    {
        _roleDomainService = roleDomainService;
        _unitOfWork = unitOfWork;
    }

    public override void Configure()
    {
        Post("/roles/create");
        AllowAnonymous(); // تغییر دهید در صورت نیاز
    }

    public override async Task HandleAsync(CreateRoleRequest req, CancellationToken ct)
    {
        // بررسی اعتبار درخواست
        if (string.IsNullOrWhiteSpace(req.Title))
        {
            ThrowError("Title cannot be empty.");
        }

            using var uow = _unitOfWork.Begin();
        try
        {
            // شروع تراکنش

            // ایجاد نقش جدید با استفاده از Domains Service
            var roleResult = await _roleDomainService.CreateRoleAsync(new RoleEntity(req.Id, req.Title));

            if (roleResult.IsFailure)
            {
                await SendErrorsAsync(cancellation: ct);
                return;
            }
            
            // پایان تراکنش
            await uow.CommitAsync(ct);

            // ارسال پاسخ موفقیت
            await SendAsync(new CreateRoleResponse(roleResult.Data.RoleId, "Role created successfully."), cancellation: ct);
        }
        catch (Exception ex)
        {
            await uow.RollbackAsync(ct);

            // مدیریت خطا
            ThrowError($"Error while creating role: {ex.Message}");
        }
    }
}