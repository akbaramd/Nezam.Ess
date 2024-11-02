using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bonyan.Layer.Application.Services;
using Bonyan.Security.Claims;
using Bonyan.UserManagement.Application.Dtos;
using Bonyan.UserManagement.Domain.Repositories;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Auth
{
    public class AuthService : ApplicationService, IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUserRepository UserRepository =>
            LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        public async Task<AuthJwtDto> LoginAsync(AuthLoginDto authLoginDto,
            CancellationToken cancellationToken = default)
        {
            var user = await UserRepository.FindOneAsync(x => x.UserName.Equals(authLoginDto.Username));

            if (user == null || !user.VerifyPassword(authLoginDto.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var jwtToken = JwtBearer.CreateToken(
                o =>
                {
                    o.SigningKey = "asdsldaosjdisd2364723hy54u23g5835t237854234";
                    o.ExpireAt = DateTime.UtcNow.AddDays(1);

                    o.User.Claims.Add((BonyanClaimTypes.UserName, user.UserName));
                    o.User.Claims.Add((BonyanClaimTypes.PhoneNumber, user.PhoneNumber?.Number ?? ""));
                    o.User.Claims.Add((BonyanClaimTypes.UserId, user.Id.Value.ToString() ?? ""));
                    o.User.Claims.Add((BonyanClaimTypes.Email, user.Email?.Address.ToString() ?? ""));
                });


            return new AuthJwtDto
            {
                AccessToken = jwtToken,
                UserId = user.Id
            };
        }

        public async Task<BonyanUserDto> CurrentUserProfileAsync(CancellationToken cancellationToken = default)
        {
            var user = await UserRepository.FindOneAsync(x => x.UserName == CurrentUser.UserName);

            return Mapper.Map<UserEntity, BonyanUserDto>(user ?? throw new InvalidOperationException());
        }
    }
}