namespace Nezam.Modular.ESS.Identity.Domain.Shared.Roles;

public  static class RoleConst
{
    public const string EmployerTitle = "Employer";

    public const string EngineerTitle = "Engineer";
    public static RoleId EngineerRoleId  = RoleId.NewId("engineer");
    public static RoleId EmployerRoleId  = RoleId.NewId("employer");
}