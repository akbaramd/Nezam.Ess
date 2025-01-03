using Nezam.EES.Service.Identity.Domains.Departments;
using Nezam.EES.Service.Identity.Domains.Departments.Repositories;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Roles.DomainServices;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Domains.Users.DomainServices;
using Nezam.EEs.Shared.Domain.Identity.Roles;
using Nezam.EEs.Shared.Domain.Identity.User;
using Nezam.EEs.Shared.Domain.Identity.User.ValueObjects;
using Payeh.SharedKernel.UnitOfWork;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Seeds
{
    /// <summary>
    /// This background service seeds some initial data for identity:
    /// 1) Creates an 'admin' role.
    /// 2) Ensures an 'admin' user (username='admin' / pass='Admin@123456').
    /// 3) Seeds a static array of Persian department names.
    /// 4) Adds the admin user to the "توسعه نرم افزار" department.
    /// </summary>
    public class IdentitySeedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        // A static list of Persian department names to seed in the system
        private static readonly string[] _departmentNames = new[]
        {
            "خدمات مهندسی (نظارت)",
            "خدمات مهندسی (مجریان)",
            "خدمات مهندسی (طراحی)",
            "دبیرخانه",
            "پشتیبانی فنی"
        };

        public IdentitySeedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// The core worker method that runs at service startup to seed data.
        /// </summary>
        /// <param name="stoppingToken">Cancellation token to stop the background service.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create a DI scope to retrieve scoped services (domain services, etc.)
            using var scope = _serviceProvider.CreateScope();

            // Get domain services (User, Role, Department) & UoW manager from DI
            var userDomainService = scope.ServiceProvider.GetRequiredService<IUserDomainService>();
            var roleDomainService = scope.ServiceProvider.GetRequiredService<IRoleDomainService>();
            var departmentRepository = scope.ServiceProvider.GetRequiredService<IDepartmentRepository>();
            var unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

            // Begin a unit of work
            using var uow = unitOfWorkManager.Begin();

            // -----------------------------------------------------------------------
            // 1) Create or ensure 'admin' role
            // -----------------------------------------------------------------------
            var adminRoleId = RoleId.NewId("admin");
            await roleDomainService.CreateRoleAsync(new RoleEntity(adminRoleId, "مدیر سیستم"));

            // -----------------------------------------------------------------------
            // 2) Seed Departments from static list
            // -----------------------------------------------------------------------
            var seededDepartments = new List<DepartmentEntity>();

            // Iterate over each department name in the static array
            foreach (var departmentName in _departmentNames)
            {
                var existingDepartment = await departmentRepository
                    .FindOneAsync(dep => dep.Title == departmentName);

                if (existingDepartment == null)
                {
                    // Department does not exist, so create a new one
                    var newDepartment = new DepartmentEntity(
                        DepartmentId.NewId(),
                        departmentName
                    );

                    await departmentRepository.AddAsync(newDepartment, true);
                    seededDepartments.Add(newDepartment);
                }
                else
                {
                    // Already exists in DB
                    seededDepartments.Add(existingDepartment);
                }
            }

            // Retrieve a reference to "توسعه نرم افزار" after seeding
            // (Should be guaranteed to exist now)
            var developmentDepartment = seededDepartments
                .FirstOrDefault(x => x.Title == "پشتیبانی فنی");

            // -----------------------------------------------------------------------
            // 3) Check or create 'admin' user
            // -----------------------------------------------------------------------
            var findUserResult = await userDomainService.GetUserByUsernameAsync(UserNameId.NewId("admin"));
            
            if (findUserResult.IsSuccess && findUserResult.Data != null)
            {
                // User already exists
                var adminUser = findUserResult.Data;

                // Update admin profile info
                adminUser.UpdateProfile(new UserProfileValue("admin", "administrator"));
                adminUser.CanNotDelete(); // Prevent deletion
                await userDomainService.UpdateAsync(adminUser);

                // Assign admin role if not already assigned
                await userDomainService.AssignRoleAsync(adminUser, new[] { adminRoleId });

                // Assign the user to the "توسعه نرم افزار" department (if found)
                if (developmentDepartment != null)
                {
                    adminUser.AddDepartment(developmentDepartment);
                    await userDomainService.UpdateAsync(adminUser);
                }
            }
            else
            {
                // Create new user
                var profile = new UserProfileValue("admin", "administrator");
                var newAdminUser = new UserEntity(
                    UserId.NewId(),
                    UserNameId.NewId("admin"),
                    new UserPasswordValue("Admin@123456"), // Password in plain text here (should be hashed in production!)
                    profile,
                    new UserEmailValue("admin@admin.com")
                );
                
                // Create the new user via domain service
                var createUserResult = await userDomainService.Create(newAdminUser);
                
                // Assign admin role
                await userDomainService.AssignRoleAsync(createUserResult.Data, new[] { adminRoleId });

                // Add user to "توسعه نرم افزار" department (if found)
                if (developmentDepartment != null)
                {
                    createUserResult.Data.AddDepartment(developmentDepartment);
                    await userDomainService.UpdateAsync(createUserResult.Data);
                }
            }

            // -----------------------------------------------------------------------
            // 4) Commit transaction
            // -----------------------------------------------------------------------
            await uow.CommitAsync(stoppingToken);
        }
    }
}
