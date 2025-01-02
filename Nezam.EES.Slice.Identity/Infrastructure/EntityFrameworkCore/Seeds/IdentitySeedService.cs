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
    /// - Creates an 'admin' role.
    /// - Ensures an admin user.
    /// - Creates a 'توسعه نرم افزار' department if not exists.
    /// - Adds the admin user to that department.
    /// </summary>
    public class IdentitySeedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

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

            // ----------------------------
            // 1) Create or ensure 'admin' role
            // ----------------------------
            var roleId = RoleId.NewId("admin");
            await roleDomainService.CreateRoleAsync(new RoleEntity(roleId, "مدیر سیستم"));

            // ----------------------------
            // 2) Create or ensure 'توسعه نرم افزار' department
            // ----------------------------
            DepartmentEntity department  = null;
            var departmentName = "توسعه نرم افزار";
            var existingDeptResult = await departmentRepository.FindOneAsync(x=>x.Title == departmentName);
      
            
            if ( existingDeptResult != null)
            {
                department = existingDeptResult;
            }
            else
            {
                department =  new DepartmentEntity(
                    DepartmentId.NewId(), // or any unique string 
                    departmentName
                );;
                
                await departmentRepository.AddAsync(department,true);
            }

            // ----------------------------
            // 3) Check or create 'admin' user
            // ----------------------------
            var findUserResult = await userDomainService.GetUserByUsernameAsync(UserNameId.NewId("admin"));
            
            if (findUserResult.IsSuccess && findUserResult.Data != null)
            {
                // User already exists
                var user = findUserResult.Data;
                
                // Update admin profile info
                user.UpdateProfile(new UserProfileValue("admin", "administrator"));
                user.CanNotDelete(); // Prevent deletion
                await userDomainService.UpdateAsync(user);

                // Assign admin role if not already assigned
                await userDomainService.AssignRoleAsync(user, new[] { roleId });

                // Assign to 'توسعه نرم افزار' department
                user.AddDepartment(department);
                await userDomainService.UpdateAsync(user);
            }
            else
            {
                // Create new user
                var profile = new UserProfileValue("admin", "administrator");
                var user = new UserEntity(
                    UserId.NewId(),
                    UserNameId.NewId("admin"),
                    new UserPasswordValue("Admin@123456"), // Password in plain text here (should be hashed in production!)
                    profile,
                    new UserEmailValue("admin@admin.com")
                );
                
                // Create the new user via domain service
                var createUserResult = await userDomainService.Create(user);
                
                // Assign admin role
                await userDomainService.AssignRoleAsync(createUserResult.Data, new[] { roleId });

                // Assign to 'توسعه نرم افزار' department
                createUserResult.Data.AddDepartment(department);
                await userDomainService.UpdateAsync(createUserResult.Data);
            }

            // ----------------------------
            // Commit transaction
            // ----------------------------
            await uow.CommitAsync(stoppingToken);
        }
    }
}
