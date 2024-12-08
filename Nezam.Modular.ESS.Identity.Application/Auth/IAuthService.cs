﻿using Bonyan.Layer.Application.Services;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Auth;

public interface IAuthService : IBonApplicationService
{
    public Task<AuthJwtResult> 
        LoginAsync(AuthLoginDto authLoginDto,CancellationToken cancellationToken = default);
    
    
    public Task<UserDtoWithDetail> CurrentUserProfileAsync(CancellationToken cancellationToken = default);
    
    public Task<AuhForgetPasswordResult> 
        ForgetPasswordAsync(AuhForgetPasswordDto forgetPasswordDto,CancellationToken cancellationToken = default);
    
    public Task 
        ResetPasswordAsync(AuthResetPasswordDto authResetPassword,CancellationToken cancellationToken = default);
}