using Bonyan.AspNetCore.Job;
using Bonyan.UnitOfWork;
using Bonyan.UserManagement.Domain.Enumerations;
using Bonyan.UserManagement.Domain.ValueObjects;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Application.Engineers.Jobs;

public class EngineerSynchronizerWorker(
    IBonUnitOfWorkManager workManager,
    ILogger<EngineerSynchronizerWorker> logger,
    RoleManager roleManager,
    UserDomainService userDomainService)
    : IBonWorker, IBonUnitOfWorkEnabled
{
    private readonly IBonUnitOfWorkManager _workManager =
        workManager ?? throw new ArgumentNullException(nameof(workManager));

    private readonly RoleManager _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    private readonly UserDomainService _userDomainService = userDomainService ?? throw new ArgumentNullException(nameof(userDomainService));

    private readonly ILogger<EngineerSynchronizerWorker> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));


    [BonUnitOfWork]
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        // Ensure the engineer role exists
        if (!await _roleManager.RoleExistsAsync(RoleConst.EngineerRoleId))
        {
            await _roleManager.CreateAsync(RoleConst.EngineerRoleId, RoleConst.EngineerTitle);
        }

        using var uow = _workManager.Begin();

        const string connectionString =
            "Data Source=85.185.6.4;Initial Catalog=map;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;";

        await using var connection = new SqlConnection(connectionString);
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

        foreach (var engineerDto in engineerDtos)
        {
            var username = !string.IsNullOrWhiteSpace(engineerDto.MelliCode) && engineerDto.MelliCode.Length >= 9
                ? engineerDto.MelliCode
                : engineerDto.OzviyatNo;

            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Engineer with ID {Id} has an invalid username and will be skipped.",
                    engineerDto.Id);
                continue;
            }


            try
            {
                var existingUser = await _userDomainService.FindByUserNameAsync(username);
                UserEntity userEntity;

                if (existingUser == null)
                {
                    // Create a new UserEntity for a new engineer
                    userEntity = UserEntity.Create(BonUserId.CreateNew(), username);
                    userEntity.ChangeStatus(UserStatus.PendingApproval);

                    var password = !string.IsNullOrEmpty(engineerDto.Password) && engineerDto.Password.Length >= 3
                        ? engineerDto.Password
                        : engineerDto.OzviyatNo;

                    await _userDomainService.CreateAsync(userEntity, password ?? UserConst.DefaultPassword);
                    await _userDomainService.AssignRolesAsync(userEntity, RoleConst.EngineerRoleId);
                }
                else
                {
                    // Update the existing UserEntity
                    userEntity = existingUser;
                    userEntity.SetPassword(
                        (!string.IsNullOrEmpty(engineerDto.Password) && engineerDto.Password.Length >= 3
                            ? engineerDto.Password
                            : engineerDto.OzviyatNo) ?? UserConst.DefaultPassword);

                    await _userDomainService.AssignRolesAsync(userEntity, RoleConst.EngineerRoleId);
                    await _userDomainService.UpdateAsync(userEntity);
                }

                // Assign or update the engineer profile via UserEntity
                userEntity.AssignOrUpdateEngineer(engineerDto.Name, engineerDto.Fname, engineerDto.OzviyatNo);
                await _userDomainService.UpdateAsync(userEntity);

                processedCount++;
                _logger.LogInformation("Processed {ProcessedCount}/{TotalCount} engineers.", processedCount,
                    engineerDtos.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error occurred while processing engineer with membership code {MembershipCode}.",
                    engineerDto.OzviyatNo);
            }
        }

        await uow.CompleteAsync(cancellationToken);
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