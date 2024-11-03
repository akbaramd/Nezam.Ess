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
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Jobs
{
    [CronJob("* 1 * * *")]
    public class EmployerSynchronizerJob : IJob
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWorkManager _workManager;
        private readonly ILogger<EmployerSynchronizerJob> _logger;

        public EmployerSynchronizerJob(
            IEmployerRepository employerRepository,
            IUserRepository userRepository,
            IUnitOfWorkManager workManager,
            ILogger<EmployerSynchronizerJob> logger, IRoleRepository roleRepository)
        {
            _employerRepository = employerRepository;
            _userRepository = userRepository;
            _workManager = workManager;
            _logger = logger;
            _roleRepository = roleRepository;
        }

        [UnitOfWork]
        public async Task ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var enginerRole = await _roleRepository.FindOneAsync(x => x.Name.Equals("employer"));

            if (enginerRole==null)
            {
                enginerRole = new RoleEntity(RoleId.CreateNew(), "employer", "Employer");
                await _roleRepository.AddAsync(enginerRole);
            }
            
            // Connection string to the database
            const string connectionString =
                "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";

            await using (var connection = new SqlConnection(connectionString))
            {
                // Query all employers from tbl_employers
                var employers = await connection.QueryAsync<KarbaranDataDto>(
                    @$"select 
	tbl_karbaran.kname as UserName ,
	tbl_karbaran.pwd as Password ,
	tbl_karbaran.comment as FullName 
from tbl_karbaran where is_personel = 1");

                int processedCount = 0;
                var employerDtos = employers.ToList();
                var count = employerDtos.Count();
                foreach (var employerDto in employerDtos)
                {
                    var username =
                        employerDto.UserName;

                    if (username == null)
                    {
                        _logger.LogWarning(
                            "Employer with Username: {username} has a null Username and will be skipped.",
                            employerDto.UserName);
                        continue;
                    }

                    try
                    {
                        // Check if the user already exists based on registration number or unique field
                        var existingUser =
                            await _userRepository.FindOneAsync(x => x.UserName == username);

                        UserEntity userEntity;
                        if (existingUser == null)
                        {
                            // Create a new user if not exists
                            userEntity = new UserEntity(UserId.CreateNew(), username);
                            userEntity.SetPassword(employerDto.Password is { Length: >= 3 }
                                ? employerDto.Password
                                : employerDto.UserName);
                            userEntity.ChangeStatus(UserStatus.PendingApproval);
                            userEntity.TryAssignRole(enginerRole);
                            await _userRepository.AddAsync(userEntity,true);
                        }
                        else
                        {
                            userEntity = existingUser;
                            userEntity.SetPassword(employerDto.Password is { Length: >= 3 }
                                ? employerDto.Password
                                : employerDto?.UserName ?? string.Empty);
                          
                            userEntity.TryAssignRole(enginerRole);
                            await _userRepository.UpdateAsync(userEntity,true);
                        }

                        // Check if the employer entity already exists
                        var existingEmployer = await _employerRepository.FindOneAsync(x => x.UserId == userEntity.Id);
                        if (existingEmployer == null)
                        {
                            // Create and add a new employer entity
                            var newEmployer = new EmployerEntity(EmployerId.CreateNew(), userEntity, employerDto?.FullName,
                                string.Empty);
                            await _employerRepository.AddAsync(newEmployer,true);
                        }

                        processedCount++;

                        _logger.LogInformation("Processed {processedCount} employers from {count}.", processedCount, count);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "An error occurred while processing employer with OzviyatNo {OzviyatNo}.",
                            employerDto.UserName);
                    }
                }

                _logger.LogInformation("Synchronization completed. Total processed: {TotalCount}", processedCount);
            }
        }
    }

    // DTO for Dapper query
    public class KarbaranDataDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
 
    }
}