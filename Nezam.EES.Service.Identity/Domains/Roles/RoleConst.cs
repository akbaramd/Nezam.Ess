using Nezam.EEs.Shared.Domain.Identity.Roles;

namespace Nezam.EES.Service.Identity.Domains.Roles;

public  static class RoleConst
{
    public const string EmployerTitle = "Employer";

    public const string EngineerTitle = "Engineer";
    public static RoleId EngineerRoleId  = RoleId.NewId("engineer");
    public static RoleId EmployerRoleId  = RoleId.NewId("employer");
}