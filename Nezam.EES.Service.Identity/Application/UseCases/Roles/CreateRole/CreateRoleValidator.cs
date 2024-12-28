using FastEndpoints;
using FluentValidation;

namespace Nezam.EES.Service.Identity.Application.UseCases.Roles.CreateRole;

public class CreateRoleValidator : Validator<CreateRoleRequest>
{
    public CreateRoleValidator()
    {
        // Validate RoleId
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("شناسه نقش نمی‌تواند خالی باشد");

        RuleFor(x => x.Id.Value)
            .NotEmpty()
            .WithMessage("شناسه نقش باید مقدار داشته باشد")
            .Must(value => value.Length == 36) // Assuming GUID format
            .WithMessage("شناسه نقش باید یک مقدار GUID معتبر باشد");

        // Validate Title
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("عنوان نقش نمی‌تواند خالی باشد")
            .MinimumLength(3)
            .WithMessage("عنوان نقش باید حداقل ۳ کاراکتر باشد")
            .MaximumLength(50)
            .WithMessage("عنوان نقش نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")
            .Matches(@"^[\w\s]+$")
            .WithMessage("عنوان نقش باید فقط شامل حروف، اعداد و فاصله باشد");
    }
}