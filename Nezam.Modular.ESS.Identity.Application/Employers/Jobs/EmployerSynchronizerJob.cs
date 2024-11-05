using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Bonyan.AspNetCore.Job;
using Dapper;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UnitOfWork;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Nezam.Modular.ESS.Identity.Domain.DomainServices;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Jobs
{
    [CronJob("0/1 * * * *")]
    public class EmployerSynchronizerJob : IJob
    {
        private const string ConnectionString = "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";
        
        private readonly IEmployerRepository _employerRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _workManager;
        private readonly ILogger<EmployerSynchronizerJob> _logger;

        public EmployerSynchronizerJob(
            IEmployerRepository employerRepository,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager workManager,
            ILogger<EmployerSynchronizerJob> logger)
        {
            _employerRepository = employerRepository ?? throw new ArgumentNullException(nameof(employerRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _workManager = workManager ?? throw new ArgumentNullException(nameof(workManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            using var uow = _workManager.Begin();
            try
            {
                await _roleManager.CreateAsync(RoleConstants.EmployerName, RoleConstants.EmployerTitle);
                
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
                            userEntity = new UserEntity(UserId.CreateNew(), employerDto.UserName);
                            userEntity.ChangeStatus(UserStatus.PendingApproval);

                            string password = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                                ? employerDto.Password
                                : employerDto.UserName;

                            await _userManager.CreateAsync(userEntity, password);
                            await _userManager.AssignRoles(userEntity, RoleConstants.EmployerName);
                        }
                        else
                        {
                            userEntity = existingUser;
                            string newPassword = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                                ? employerDto.Password
                                : employerDto.UserName;

                            userEntity.SetPassword(newPassword);
                            await _userManager.AssignRoles(userEntity, RoleConstants.EmployerName);
                            await _userManager.UpdateAsync(userEntity);
                        }

                        var existingEmployer = await _employerRepository.FindOneAsync(x => x.UserId == userEntity.Id);
                        if (existingEmployer == null)
                        {
                            var newEmployer = new EmployerEntity(EmployerId.CreateNew(), userEntity, employerDto.FullName ?? string.Empty, string.Empty);
                            await _employerRepository.AddAsync(newEmployer, true);
                        }

                        processedCount++;
                        _logger.LogInformation("Processed {ProcessedCount}/{TotalEmployers} employers.", processedCount, totalEmployers);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while processing employer with username {Username}.", employerDto.UserName);
                    }
                }

                _logger.LogInformation("Employer synchronization completed. Total processed: {TotalCount}", processedCount);
                await uow.CompleteAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the synchronization job.");
            }
        }
    }

    public class KarbaranDataDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
    }
}
