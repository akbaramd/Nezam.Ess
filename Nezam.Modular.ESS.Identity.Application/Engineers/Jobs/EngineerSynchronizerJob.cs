using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Bonyan.AspNetCore.Job;
using Dapper;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Bonyan.Layer.Domain.ValueObjects;
using Bonyan.UnitOfWork;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Nezam.Modular.ESS.Identity.Domain.Roles;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Jobs
{
    public class EngineerSynchronizerJob : IJob
    {
        private readonly IEngineerRepository _engineerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWorkManager _workManager;
        private readonly ILogger<EngineerSynchronizerJob> _logger;

        public EngineerSynchronizerJob(
            IEngineerRepository engineerRepository,
            IUserRepository userRepository,
            IUnitOfWorkManager workManager,
            ILogger<EngineerSynchronizerJob> logger, IRoleRepository roleRepository)
        {
            _engineerRepository = engineerRepository;
            _userRepository = userRepository;
            _workManager = workManager;
            _logger = logger;
            _roleRepository = roleRepository;
        }

        [UnitOfWork]
        public async Task ExecuteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var enginerRole = await _roleRepository.FindOneAsync(x => x.Name.Equals("engineer"));

            if (enginerRole==null)
            {
                enginerRole = new RoleEntity(RoleId.CreateNew(), "engineer", "Engineer");
                await _roleRepository.AddAsync(enginerRole);
            }
            
            // Connection string to the database
            const string connectionString =
                "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";

            await using (var connection = new SqlConnection(connectionString))
            {
                // Query all engineers from tbl_engineers
                var engineers = await connection.QueryAsync<EngineerDto>(
                    @$"SELECT Top 1000 
                        id AS Id, 
                        name AS Name, 
                        fname AS Fname, 
                           melli_cod AS MelliCode, 
                        mob_no AS MobileNumber, 
                        ozviyat_no AS OzviyatNo, 
                        password AS Password, 
                        e_mail AS Email
                    FROM tbl_engineers
                    ORDER BY id DESC;");

                int processedCount = 0;
                var engineerDtos = engineers.ToList();
                var count = engineerDtos.Count();
                foreach (var engineerDto in engineerDtos)
                {
                    var username =
                        (!string.IsNullOrWhiteSpace(engineerDto.MelliCode) && engineerDto.MelliCode.Length >= 9)
                            ? engineerDto.MelliCode
                            : engineerDto.OzviyatNo;

                    if (username == null)
                    {
                        _logger.LogWarning(
                            "Engineer with Username: {username} has a null Username and will be skipped.",
                            engineerDto.Id);
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
                            userEntity.SetPassword(engineerDto.Password is { Length: >= 3 }
                                ? engineerDto.Password
                                : engineerDto.OzviyatNo);
                            if (PhoneNumber.IsValidPhoneNumber(engineerDto.MobileNumber))
                                userEntity.SetPhoneNumber(new PhoneNumber(engineerDto.MobileNumber.TrimStart('0')));
                            if (Email.IsValidEmail(engineerDto.Email))
                                userEntity.SetEmail(new Email(engineerDto.Email));
                            userEntity.ChangeStatus(UserStatus.PendingApproval);
                            userEntity.TryAssignRole(enginerRole);
                            await _userRepository.AddAsync(userEntity,true);
                        }
                        else
                        {
                            userEntity = existingUser;
                            userEntity.SetPassword(engineerDto.Password is { Length: >= 3 }
                                ? engineerDto.Password
                                : engineerDto?.OzviyatNo ?? string.Empty);
                            if (PhoneNumber.IsValidPhoneNumber(engineerDto.MobileNumber))
                                userEntity.SetPhoneNumber(new PhoneNumber(engineerDto.MobileNumber.TrimStart('0')));
                            if (Email.IsValidEmail(engineerDto.Email))
                                userEntity.SetEmail(new Email(engineerDto.Email));
                            userEntity.TryAssignRole(enginerRole);
                            await _userRepository.UpdateAsync(userEntity,true);
                        }

                        // Check if the engineer entity already exists
                        var existingEngineer = await _engineerRepository.FindOneAsync(x => x.UserId == userEntity.Id);
                        if (existingEngineer == null)
                        {
                            // Create and add a new engineer entity
                            var newEngineer = new EngineerEntity(EngineerId.CreateNew(), userEntity, engineerDto?.Name,
                                engineerDto?.Fname,
                                engineerDto.OzviyatNo.ToString());
                            await _engineerRepository.AddAsync(newEngineer,true);
                        }

                        processedCount++;

                        _logger.LogInformation("Processed {processedCount} engineers from {count}.", processedCount, count);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "An error occurred while processing engineer with OzviyatNo {OzviyatNo}.",
                            engineerDto.OzviyatNo);
                    }
                }

                _logger.LogInformation("Synchronization completed. Total processed: {TotalCount}", processedCount);
            }
        }
    }

    // DTO for Dapper query
    public class EngineerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Fname { get; set; }
        public string? MobileNumber { get; set; }
        public string? OzviyatNo { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string MelliCode { get; set; }
    }
}