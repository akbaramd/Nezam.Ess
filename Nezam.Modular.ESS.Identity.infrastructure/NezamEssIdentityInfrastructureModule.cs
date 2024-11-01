using System.Text;
using Bonyan.EntityFrameworkCore;
using Bonyan.Modularity;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.Repositories;
using Bonyan.UserManagement.Domain.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nezam.Modular.ESS.Identity.Application;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Identity.infrastructure.Data;
using Nezam.Modular.ESS.Identity.infrastructure.Data.Repository;

namespace Nezam.Modular.ESS.Identity.infrastructure;

public class NezamEssIdentityInfrastructureModule : WebModule
{
    public NezamEssIdentityInfrastructureModule()
    {
        DependOn<BonyanUserManagementEntityFrameworkModule<UserEntity>>();
        DependOn<NezamEssIdentityApplicationModule>();
    }

    public override Task OnConfigureAsync(ServiceConfigurationContext context)
    {
        context.AddBonyanDbContext<IdentityDbContext>(c => { c.AddDefaultRepositories(true); });
        context.Services.AddTransient<IRoleRepository, RoleRepository>();
        context.Services.AddTransient<IUserRepository, UserRepository>();

        var configuration = context.Services.BuildServiceProvider().GetRequiredService<IConfiguration>() ?? throw new ArgumentNullException("context.Services.GetRequiredService<IConfiguration>()");
        
        var jwtSettings = configuration.GetSection("JwtSettings");
        context.Services.AddAuthorization();
        context.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("JWT authentication failed: " + context.Exception);
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

        context.Services.AddAuthorization();

        return base.OnConfigureAsync(context);
    }
    public override async Task OnPostApplicationAsync(ApplicationContext context)
    {
        await SeedRoles(context.RequireService<IRoleRepository>());
        await SeedUser(context.RequireService<IUserRepository>());
        
        await base.OnPostApplicationAsync(context);
    }

    private async Task SeedUser(IUserRepository requireService)
    {
        if (!await requireService.ExistsAsync(x=>x.UserName.Equals("akbarsafari00")))
        {
            var user = new UserEntity(UserId.CreateNew(), "akbarsafari00");
            user.ChangeStatus(UserStatus.Active);
            user.SetPassword("Aa@123456");
            await requireService.AddAsync(user);
        }
    }

    private async Task SeedRoles(IRoleRepository requireService)
    {
        if (!await requireService.ExistsAsync(x => x.Name.Equals("admin")))
        {   
            await requireService.AddAsync(new RoleEntity(RoleId.CreateNew(),"admin", "میدریت"));
        }
    }
}