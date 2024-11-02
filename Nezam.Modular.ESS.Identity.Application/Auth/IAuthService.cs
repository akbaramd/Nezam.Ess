using Bonyan.Layer.Application.Services;
using Bonyan.UserManagement.Application.Dtos;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Auth;

public interface IAuthService : IApplicationService
{
    public Task<AuthJwtResult> 
        LoginAsync(AuthLoginDto authLoginDto,CancellationToken cancellationToken = default);
    
    
    public Task<BonyanUserDto> 
        CurrentUserProfileAsync(CancellationToken cancellationToken = default);
    
    public Task<AuhForgetPasswordResult> 
        ForgetPasswordAsync(AuhForgetPasswordDto forgetPasswordDto,CancellationToken cancellationToken = default);
    
    public Task 
        ResetPasswordAsync(AuthResetPasswordDto authResetPassword,CancellationToken cancellationToken = default);
}