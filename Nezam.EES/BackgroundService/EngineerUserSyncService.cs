using System.Data;
using Dapper;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Gateway.BackgroundService;

public class EngineerUserSyncService : Microsoft.Extensions.Hosting.BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EngineerUserSyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
            var userDomainService = scope.ServiceProvider.GetRequiredService<IUserDomainService>();
            var roleDomainService = scope.ServiceProvider.GetRequiredService<IRoleDomainService>();
            var engineerRoleId = RoleId.NewId("engineer");
            using (var uowInit = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Begin())
            {
                // Ensure the Engineer role exists

                var roleCheck = await roleDomainService.GetRoleByIdAsync(engineerRoleId);
                if (!roleCheck.IsSuccess)
                {
                    await roleDomainService.CreateRoleAsync(new RoleEntity(engineerRoleId, "مهندس"));
                }
                else
                {
                    engineerRoleId = roleCheck.Data.RoleId;
                }

                await uowInit.CommitAsync(stoppingToken);
            }


            // Fetch the top 500 engineers from the tbl_engineers table
            const string query = "SELECT  ozviyat_no, name, password, fname, e_mail FROM tbl_engineers";
            var engineers = await dbConnection.QueryAsync(query);

            int index = 0; // Initialize the index
            var total = engineers.Count();
            foreach (var engineer in engineers)
            {
                // Use a new scope for each engineer to ensure a fresh DbContext instance
                using (var innerScope = _serviceProvider.CreateScope())
                {
                    // Ensure a new UnitOfWork with a separate DbContext
                    using (var uow = innerScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Begin())
                    {
                        try
                        {
                            var username = engineer.ozviyat_no.ToString();
                            var firstName = engineer.name;
                            var password = !string.IsNullOrWhiteSpace(engineer.password) &&
                                           UserPasswordValue.IsValidPassword(engineer.password)
                                ? engineer.password
                                : username; // Fallback to ozviyat_no if no password
                            var lastName = engineer.fname;
                            var email = engineer.e_mail;

                            userDomainService = innerScope.ServiceProvider.GetRequiredService<IUserDomainService>();

                            // Check if the user already exists
                            var userCheck = await userDomainService.GetUserByUsernameAsync(UserNameId.NewId(username));
                            if (!userCheck.IsSuccess)
                            {
                                // Create a new user
                                var profile = new UserProfileValue(firstName, lastName);
                                var user = new UserEntity(
                                    UserId.NewId(),
                                    UserNameId.NewId(username),
                                    new UserPasswordValue(password), // Default password
                                    profile,
                                    !string.IsNullOrWhiteSpace(email) && UserEmailValue.IsValidEmail(email)
                                        ? new UserEmailValue(email)
                                        : null
                                );
                                var userCreateResult = await userDomainService.Create(user);

                    
                                await userDomainService.AssignRoleAsync(userCreateResult.Data,
                                    new[] { engineerRoleId });
                            }

                            // Commit changes for this engineer
                            await uow.CommitAsync(stoppingToken);

                            // Log the progress
                            Console.WriteLine($"Processed Engineer {index + 1}: {total}");

                            index++; // Increment the index after processing each engineer
                        }
                        catch (Exception ex)
                        {
                            // Rollback on error
                            Console.WriteLine($"Error processing engineer {engineer.ozviyat_no}: {ex.Message}");
                        }
                    }
                } // Disposes the inner scope and commits the UnitOfWork
            }

            // Log when the process is finished
            Console.WriteLine("Engineer synchronization process completed.");
        }
    }
}