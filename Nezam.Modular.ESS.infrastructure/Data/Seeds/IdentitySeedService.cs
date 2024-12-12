using Microsoft.Extensions.Hosting;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.Modular.ESS.Infrastructure.Data.Seeds;

public class IdentitySeedService : BackgroundService
{
    private readonly IUserDomainService _domainService;
    private readonly IUnitOfWork _unitOfWorkManager;

    public IdentitySeedService(IUserDomainService domainService, IUnitOfWork unitOfWorkManager)
    {
        _domainService = domainService;
        _unitOfWorkManager = unitOfWorkManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        var find = await _domainService.GetUserByUsernameAsync(new UserNameValue("admin"));

        if (find.IsSuccess)
        {
            var user = find.Data;
            user.UpdateProfile(new UserProfileValue("/default","admin","administrator"));
            await _domainService.UpdateAsync(user);
        }
        else
        {
            var profile = new UserProfileValue("/default","admin","administrator");
            var user = new UserEntity(UserId.NewId(), new UserNameValue("admin"), new UserPasswordValue("Admin@123456"),
                profile, new UserEmailValue("admin@admin.com"));
            await _domainService.Create(user);
        }

        await _unitOfWorkManager.CommitAsync();

    }
}