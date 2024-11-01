using Bonyan.Layer.Application.Services;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;

namespace Nezam.Modular.ESS.Identity.Application.Auth;

public interface IAuthService : IApplicationService
{
    public Task<AuthJwtDto> 
        LoginAsync(AuthLoginDto authLoginDto,CancellationToken cancellationToken = default);
}