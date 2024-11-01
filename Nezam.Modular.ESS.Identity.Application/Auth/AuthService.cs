using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bonyan.Layer.Application.Services;
using Bonyan.Security.Claims;
using Bonyan.UserManagement.Domain.Repositories;
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

        public async Task<AuthJwtDto> LoginAsync(AuthLoginDto authLoginDto, CancellationToken cancellationToken = default)
        {
            var user = await UserRepository.FindOneAsync(x => x.UserName.Equals(authLoginDto.Username));

            if (user == null || !user.VerifyPassword(authLoginDto.Password))
            {
                throw new UnauthorizedAccessException();
            }

            var token = GenerateToken(user);

            return new AuthJwtDto
            {
                AccessToken = token,
                UserId = user.Id
            };
        }

        private string GenerateToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(BonyanClaimTypes.UserName, user.UserName.ToString()),
                new Claim(BonyanClaimTypes.UserId, user.Id.Value.ToString()),
                new Claim(BonyanClaimTypes.Email, user.Email?.Address.ToString() ?? string.Empty),
                new Claim(BonyanClaimTypes.PhoneNumber, user.PhoneNumber?.Number.ToString() ?? string.Empty),
            };

            // Add role claims if user has roles (assuming roles are a property in your User object)
            // foreach (var role in user.Roles)
            // {
                // claims.Add(new Claim(ClaimTypes.Role, role.Name));
            // }

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
