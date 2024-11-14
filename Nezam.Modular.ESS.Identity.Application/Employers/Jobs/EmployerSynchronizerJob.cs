using Bonyan.AspNetCore.Job;
using Bonyan.UnitOfWork;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Jobs
{
    public class EmployerSynchronizerJob : IBonWorker
    {
        private const string ConnectionString = "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";
        
        private readonly UserDomainService _userDomainService;
        private readonly RoleManager _roleManager;
        private readonly IBonUnitOfWorkManager _workManager;
        private readonly ILogger<EmployerSynchronizerJob> _logger;

        public EmployerSynchronizerJob(
            UserDomainService userDomainService,
            RoleManager roleManager,
            IBonUnitOfWorkManager workManager,
            ILogger<EmployerSynchronizerJob> logger)
        {
            _userDomainService = userDomainService ?? throw new ArgumentNullException(nameof(userDomainService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _workManager = workManager ?? throw new ArgumentNullException(nameof(workManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            if (!await _roleManager.RoleExistsAsync(RoleConst.EmployerRoleId))
            {
                await _roleManager.CreateAsync(RoleConst.EmployerRoleId, RoleConst.EmployerTitle);
            }
                
            using var uow = _workManager.Begin();
            
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
                    var existingUser = await _userDomainService.FindByUserNameAsync(employerDto.UserName);
                    UserEntity userEntity;

                    if (existingUser == null)
                    {
                        // Create a new UserEntity
                        userEntity = UserEntity.Create(BonUserId.CreateNew(), employerDto.UserName);
                        userEntity.ChangeStatus(UserStatus.PendingApproval);

                        string password = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                            ? employerDto.Password
                            : employerDto.UserName;

                        await _userDomainService.CreateAsync(userEntity, password);
                        await _userDomainService.AssignRolesAsync(userEntity, RoleConst.EmployerRoleId);
                    }
                    else
                    {
                        userEntity = existingUser;
                        string newPassword = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                            ? employerDto.Password
                            : employerDto.UserName;

                        userEntity.SetPassword(newPassword);
                        await _userDomainService.AssignRolesAsync(userEntity, RoleConst.EmployerRoleId);
                        await _userDomainService.UpdateAsync(userEntity);
                    }

                    // Use the UserEntity to handle the Employer profile assignment
                    userEntity.AssignOrUpdateEmployer(employerDto.FullName,string.Empty);

                    // Persist the updated UserEntity and its Employer association through UserDomainService
                    await _userDomainService.UpdateAsync(userEntity);

                    processedCount++;
                    _logger.LogInformation("Processed {ProcessedCount}/{TotalEmployers} employers.", processedCount, totalEmployers);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing employer with username {Username}.", employerDto.UserName);
                }
            }

            await uow.CompleteAsync(cancellationToken);
            
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
