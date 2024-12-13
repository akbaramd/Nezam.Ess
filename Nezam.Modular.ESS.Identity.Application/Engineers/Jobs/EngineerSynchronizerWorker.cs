using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;
using Payeh.SharedKernel.EntityFrameworkCore.UnitOfWork;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Jobs
{
    public class EngineerSynchronizerWorker : BackgroundService
    {
        private const string ConnectionString = "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";

        private readonly IRoleDomainService _roleDomainService;
        private readonly IEngineerRepository _engineerRepository;
        private readonly IUserDomainService _userDomainService;
        private readonly IUnitOfWorkManager _workManager;
        private readonly ILogger<EngineerSynchronizerWorker> _logger;

        public EngineerSynchronizerWorker(
            IUnitOfWorkManager workManager,
            ILogger<EngineerSynchronizerWorker> logger,
            IRoleDomainService roleDomainService,
            IUserDomainService userDomainService, 
            IEngineerRepository engineerRepository)
        {
            _roleDomainService = roleDomainService ?? throw new ArgumentNullException(nameof(roleDomainService));
            _workManager = workManager ?? throw new ArgumentNullException(nameof(workManager));
            _userDomainService = userDomainService ?? throw new ArgumentNullException(nameof(userDomainService));
            _engineerRepository = engineerRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            // Ensure the engineer role exists
            using var roleUow = _workManager.Begin();
            await _roleDomainService.CreateRoleAsync(new RoleEntity(RoleConst.EngineerRoleId, RoleConst.EngineerTitle));
            await roleUow.CommitAsync();

            // Fetch the engineer data from the database
            await using var connection = new SqlConnection(ConnectionString);
            var engineers = await connection.QueryAsync<EngineerDto>(
                @"SELECT TOP 1000 
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
            var totalEngineers = engineerDtos.Count;

            foreach (var engineerDto in engineerDtos)
            {
                using var uow = _workManager.Begin();

                // Determine username (either MelliCode or OzviyatNo)
                var usernames = !string.IsNullOrWhiteSpace(engineerDto.MelliCode) && engineerDto.MelliCode.Length >= 9
                    ? engineerDto.MelliCode
                    : engineerDto.OzviyatNo;

                if (string.IsNullOrWhiteSpace(usernames))
                {
                    _logger.LogWarning("Engineer with ID {Id} has an invalid username and will be skipped.", engineerDto.Id);
                    continue;
                }

                try
                {
                    var username = new UserNameValue(usernames);

                    var user = await _userDomainService.GetUserByUsernameAsync(username);
                    var userId = user.IsSuccess ? user.Data.UserId : UserId.NewId();
                    
                    // Check if the user already exists
                    var existingEmginner = await _engineerRepository.FindOneAsync(x => x.UserName.Value == username.Value);
                    EngineerEntity userEntity;

                    // If the user doesn't exist, create a new user
                    if (existingEmginner == null)
                    {
                        var password = !string.IsNullOrEmpty(engineerDto.Password) && engineerDto.Password.Length >= 3
                            ? engineerDto.Password
                            : engineerDto.OzviyatNo;

                        // Create the new user entity
                        userEntity = new EngineerEntity(userId,engineerDto.OzviyatNo, username, new UserPasswordValue(password));

                        // Save the new engineer entity
                        await _engineerRepository.AddAsync(userEntity, true);

                        // Assign the engineer role
                        await _userDomainService.AssignRoleAsync(userEntity, RoleConst.EngineerRoleId);
                    }

                    processedCount++;
                    _logger.LogInformation("Processed {ProcessedCount}/{TotalEngineers} engineers.", processedCount, totalEngineers);

                    await uow.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing engineer with membership code {MembershipCode}.", engineerDto.OzviyatNo);
                }
            }

            _logger.LogInformation("Engineer synchronization completed. Total processed: {TotalCount}", processedCount);
        }
    }

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
