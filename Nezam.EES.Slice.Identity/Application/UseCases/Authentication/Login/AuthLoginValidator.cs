using FastEndpoints;
using FluentValidation;

namespace Nezam.EES.Service.Identity.Application.UseCases.Authentication.Login;

public class AuthLoginValidator : Validator<AuthLoginRequest>
{
    public AuthLoginValidator()
    {
        // Validate UserName
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("نام کاربری نمی‌تواند خالی باشد")
            .MinimumLength(3)
            .WithMessage("نام کاربری باید حداقل ۳ کاراکتر باشد");

        // Validate Password
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("رمز عبور نمی‌تواند خالی باشد")
            .MinimumLength(3)
            .WithMessage("رمز عبور باید حداقل 3 کاراکتر باشد");
    }
}