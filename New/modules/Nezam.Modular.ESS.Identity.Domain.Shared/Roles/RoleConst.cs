using Bonyan.IdentityManagement.Domain.Roles.ValueObjects;

namespace Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

public  static class RoleConst
{
    public const string EmployerTitle = "Employer";
    public static readonly BonRoleId EmployerRoleId =  BonRoleId.NewId("employer");

    public const string EngineerTitle = "Engineer";
    public static readonly BonRoleId EngineerRoleId =  BonRoleId.NewId("enginer");
}