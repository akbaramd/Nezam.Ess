using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Identity.Application.Employers.Jobs
{
    public class EmployerSynchronizerJob : BackgroundService
    {
        private const string ConnectionString = "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";
        
        private readonly IEmployerRepository _employerRepository;
        private readonly IRoleDomainService _roleDomainService;
        private readonly IUserDomainService _userDomainService;
        private readonly IUnitOfWorkManager _workManager;
        private readonly ILogger<EmployerSynchronizerJob> _logger;

        public EmployerSynchronizerJob(IEmployerRepository employerRepository, IRoleDomainService roleDomainService, IUserDomainService userDomainService, IUnitOfWorkManager workManager, ILogger<EmployerSynchronizerJob> logger)
        {
            _employerRepository = employerRepository;
            _roleDomainService = roleDomainService;
            _userDomainService = userDomainService;
            _workManager = workManager;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
          
            using var roleUow = _workManager.Begin();
            
            await _roleDomainService.CreateRoleAsync(new RoleEntity(RoleConst.EmployerRoleId, RoleConst.EmployerTitle));
            await using var connection = new SqlConnection(ConnectionString);
            var employers = (await connection.QueryAsync<KarbaranDataDto>(
                @"SELECT 
                    tbl_karbaran.kname AS UserName,
                    tbl_karbaran.pwd AS Password,
                    tbl_karbaran.comment AS FullName 
                  FROM tbl_karbaran 
                  WHERE is_personel = 1")).ToList();
            await roleUow.CommitAsync();
            
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
                {   using var uow = _workManager.Begin();
                    
                    var username = new UserNameValue(employerDto.UserName);
                    var user = await _userDomainService.GetUserByUsernameAsync(username);
                    var userId = user.IsSuccess ? user.Data.UserId : UserId.NewId();
                    
                    var existingUser = await _employerRepository.FindOneAsync(x=>x.UserName.Value == username.Value);
                    EmployerEntity userEntity;

                    if (existingUser == null)
                    {
                        // Create a new UserEntity
                        string password = !string.IsNullOrEmpty(employerDto.Password) && employerDto.Password.Length >= 3
                            ? employerDto.Password
                            : employerDto.UserName;
                        userEntity = new EmployerEntity( userId,username,new UserPasswordValue(password));
                        await _employerRepository.AddAsync(userEntity,true);
                        await _userDomainService.AssignRoleAsync(userEntity, [RoleConst.EmployerRoleId]);
                    }
                    processedCount++;
                    _logger.LogInformation("Processed {ProcessedCount}/{TotalEmployers} employers.", processedCount, totalEmployers);
                    await uow.CommitAsync();
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
