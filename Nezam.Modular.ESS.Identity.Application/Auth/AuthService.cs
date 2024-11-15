using Bonyan.Layer.Application.Services;
using Bonyan.Security.Claims;
using Bonyan.UserManagement.Domain.Enumerations;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;
using Nezam.Modular.ESS.Identity.Application.Auth.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Dto;
using Nezam.Modular.ESS.Identity.Application.Users.Specs;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Auth
{
    public class AuthService : BonApplicationService, IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUserRepository UserRepository =>
            LazyServiceProvider.LazyGetRequiredService<IUserRepository>();

        public IUserVerificationTokenRepository VerificationTokenRepository =>
            LazyServiceProvider.LazyGetRequiredService<IUserVerificationTokenRepository>();

        public async Task<AuthJwtResult> LoginAsync(AuthLoginDto authLoginDto,
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

                    o.User.Claims.Add((BonClaimTypes.UserName, user.UserName));
                    o.User.Claims.Add((BonClaimTypes.PhoneNumber, user.PhoneNumber?.Number ?? ""));
                    o.User.Claims.Add((BonClaimTypes.UserId, user.Id.Value.ToString() ?? ""));
                    o.User.Claims.Add((BonClaimTypes.Email, user.Email?.Address.ToString() ?? ""));

                    foreach (var role in user.Roles)
                    {
                        o.User.Roles.Add(role.RoleId.Name);
                    }
                });


            user.ChangeStatus(UserStatus.Active);
            await UserRepository.UpdateAsync(user);
            return new AuthJwtResult
            {
                AccessToken = jwtToken,
                BonUserId = user.Id
            };
        }

        public async Task<UserDtoWithDetail> CurrentUserProfileAsync(CancellationToken cancellationToken = default)
        {
            var user = await UserRepository.FindOneAsync(new UserByUsernameSpec(BonCurrentUser.UserName));

            return Mapper.Map<UserEntity, UserDtoWithDetail>(user ?? throw new InvalidOperationException());
        }

        public async Task<AuhForgetPasswordResult> ForgetPasswordAsync(AuhForgetPasswordDto forgetPasswordDto,
            CancellationToken cancellationToken = default)
        {
            var user = await UserRepository.FindOneAsync(x => x.UserName.Equals(forgetPasswordDto.Username));

            if (user == null)
            {
                throw new Exception("user not found");
            }

            var res = user.GenerateVerificationToken(UserVerificationTokenType.ForgetPassword);

            await UserRepository.UpdateAsync(user, true);

            return new AuhForgetPasswordResult()
            {
                VerificationToken = res.Token,
                VerificationTokenType = res.Type
            };
        }

        public async Task ResetPasswordAsync(AuthResetPasswordDto authResetPassword,
            CancellationToken cancellationToken = default)
        {
            var token = await VerificationTokenRepository.FindOneAsync(x =>
                x.Token.Equals(authResetPassword.VerificationToken));
            if (token == null)
            {
                throw new Exception("token not found");
            }

            var user = token.User;
            if (user == null)
            {
                throw new Exception("user not found");
            }

            if (authResetPassword.Password != authResetPassword.ConfirmPassword)
            {
                throw new Exception("password is incorrect");
            }

            user.SetPassword(authResetPassword.Password);

            var res = user.RemoveVerificationToken(token);

            await UserRepository.UpdateAsync(user, true);
        }
    }
}