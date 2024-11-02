using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Auth.Dto;

public class AuthLoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class AuhForgetPasswordDto 
{
    public string Username { get; set; }
}
public class AuhForgetPasswordResult 
{
    public string VerificationToken { get; set; }
    public UserVerificationTokenType VerificationTokenType { get; set; }
}

public class AuthResetPasswordDto 
{
    public string VerificationToken { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class AuthJwtResult
{
    public string AccessToken { get; set; }
    public UserId UserId { get; set; }
}

