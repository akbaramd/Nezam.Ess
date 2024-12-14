using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Seeds;

public class IdentitySeedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public IdentitySeedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var domainService = scope.ServiceProvider.GetRequiredService<IUserDomainService>();
            var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using var uow = unitOfWorkManager.Begin();
            var find = await domainService.GetUserByUsernameAsync(UserNameId.NewId("admin"));

            if (find.IsSuccess)
            {
                var user = find.Data;
                user.UpdateProfile(new UserProfileValue( "admin", "administrator"));
                await domainService.UpdateAsync(user);
            }
            else
            {
                var profile = new UserProfileValue( "admin", "administrator");
                var user = new UserEntity(UserId.NewId(),  UserNameId.NewId("admin"),
                    new UserPasswordValue("Admin@123456"), profile, new UserEmailValue("admin@admin.com"));
                await domainService.Create(user);
            }

            await uow.CommitAsync();
        }
    }
}