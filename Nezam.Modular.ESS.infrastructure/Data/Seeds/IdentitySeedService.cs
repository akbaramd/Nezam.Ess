using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Seeds;

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
            var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var find = await domainService.GetUserByUsernameAsync(new UserNameValue("admin"));

            if (find.IsSuccess)
            {
                var user = find.Data;
                user.UpdateProfile(new UserProfileValue("/default", "admin", "administrator"));
                await domainService.UpdateAsync(user);
            }
            else
            {
                var profile = new UserProfileValue("/default", "admin", "administrator");
                var user = new UserEntity(UserId.NewId(), new UserNameValue("admin"),
                    new UserPasswordValue("Admin@123456"), profile, new UserEmailValue("admin@admin.com"));
                await domainService.Create(user);
            }

            await unitOfWorkManager.CommitAsync();
        }
    }
}