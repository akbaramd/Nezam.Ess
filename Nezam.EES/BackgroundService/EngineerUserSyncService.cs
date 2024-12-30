using System.Data;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;

public class EngineerUserSyncService : BackgroundService
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
            var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            using var uowa = unitOfWorkManager.Begin(); // Begin UnitOfWork for the entire process

            // Ensure the Engineer role exists
            var engineerRoleId = RoleId.NewId("engineer");
            var roleCheck = await roleDomainService.GetRoleByIdAsync(engineerRoleId);
            if (!roleCheck.IsSuccess)
            {
                await roleDomainService.CreateRoleAsync(new RoleEntity(engineerRoleId, "مهندس"));
            }
            else
            {
                engineerRoleId = roleCheck.Data.RoleId;
            }

            await uowa.CommitAsync(stoppingToken);

            // Fetch all engineers from the tbl_engineers table
            const string query = "SELECT TOP 500 ozviyat_no, name, password, fname, e_mail FROM tbl_engineers";

            var engineers = await dbConnection.QueryAsync(query);

            int index = 0; // Initialize the index
            var total = engineers.Count();
            foreach (var engineer in engineers)
            {
                using var uow = unitOfWorkManager.Begin(); // Begin UnitOfWork for each engineer

                try
                {
                    
                    var username = engineer.ozviyat_no.ToString();
                    var firstName = engineer.name;
                    var password = ( engineer.password == null || engineer.password.Length < 3)? username: engineer.password;
                    var lastName = engineer.fname;
                    var email = engineer.e_mail;

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
                            (!string.IsNullOrWhiteSpace(email) && UserEmailValue.IsValidEmail(email)) ? new UserEmailValue(email) : null
                        );
                        var userCreateResult = await userDomainService.Create(user);

                        // Assign the Engineer role to the new user
                        await userDomainService.AssignRoleAsync(userCreateResult.Data, new[] { engineerRoleId });
                    }

                    await uow.CommitAsync(stoppingToken); // Commit after processing each engineer

              
                }
                catch (Exception ex)
                {
                    await uow.RollbackAsync(stoppingToken); // Rollback if any error occurs
                    Console.WriteLine($"Error processing engineer {engineer.ozviyat_no}: {ex.Message}");
                }
                
                // Log the index and information
                Console.WriteLine($"Processed Engineer {index + 1}:{total}");

                index++; // Increment the index after processing each engineer
            }

            // Log when the process is finished
            Console.WriteLine("Engineer synchronization process completed.");
        }
    }
}
