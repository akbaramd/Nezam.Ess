using Bonyan.AspNetCore.Job;
using Bonyan.UnitOfWork;
using Dapper;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Nezam.Modular.ESS.IdEntity.Domain.DomainServices;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;
using Quartz;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nezam.Modular.ESS.IdEntity.Application.Employers.Jobs
{
    public class EmployerSynchronizerJob : IBonWorker, IBonUnitOfWorkEnabled
    {
        private const string ConnectionString = "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";
        
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IBonUnitOfWorkManager _workManager;
        private readonly ILogger<EmployerSynchronizerJob> _logger;

        public EmployerSynchronizerJob(
            UserManager userManager,
            RoleManager roleManager,
            IBonUnitOfWorkManager workManager,
            ILogger<EmployerSynchronizerJob> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _workManager = workManager ?? throw new ArgumentNullException(nameof(workManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync(RoleConstants.EmployerName))
            {
                await _roleManager.CreateAsync(RoleConstants.EmployerName, RoleConstants.EmployerTitle);
            }
                
            await using var connection = new SqlConnection(ConnectionString);
            var employers = (await connection.QueryAsync<KarbaranDataDto>(
                @"SELECT 
                    tbl_karbaran.kname AS UserName,
                    tbl_karbaran.pwd AS Password,
                    tbl_karbaran.comment AS FullName 
                  FROM tbl_karbaran 
                  WHERE is_personel = 1")).ToList();

            int processedCount = 0;
            var totalEmployers = employers.Count;

            foreach (var employerDto in employers)
            {
                if (string.IsNullOrEmpty(employerDto.UserName))
                {
                    _logger.LogWarning("Employer entry with a null or empty username is skipped.");
                    continue;
                }

                try
                {
                    var existingUser = await _userManager.FindByUserNameAsync(employerDto.UserName);
                    UserEntity userEntity;

                    if (existingUser == null)
                    {
                        // Create a new UserEntity
                        userEntity = UserEntity.Create(BonUserId.CreateNew(), employerDto.UserName);
                        userEntity.ChangeStatus(UserStatus.PendingApproval);

                        string password = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                            ? employerDto.Password
                            : employerDto.UserName;

                        await _userManager.CreateAsync(userEntity, password);
                        await _userManager.AssignRolesAsync(userEntity, RoleConstants.EmployerName);
                    }
                    else
                    {
                        userEntity = existingUser;
                        string newPassword = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                            ? employerDto.Password
                            : employerDto.UserName;

                        userEntity.SetPassword(newPassword);
                        await _userManager.AssignRolesAsync(userEntity, RoleConstants.EmployerName);
                        await _userManager.UpdateAsync(userEntity);
                    }

                    // Use the UserEntity to handle the Employer profile assignment
                    userEntity.AssignOrUpdateEmployer(employerDto.FullName,string.Empty);

                    // Persist the updated UserEntity and its Employer association through UserManager
                    await _userManager.UpdateAsync(userEntity);

                    processedCount++;
                    _logger.LogInformation("Processed {ProcessedCount}/{TotalEmployers} employers.", processedCount, totalEmployers);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing employer with username {Username}.", employerDto.UserName);
                }
            }

            _logger.LogInformation("Employer synchronization completed. Total processed: {TotalCount}", processedCount);
        }
    }

    public class KarbaranDataDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
    }
}
