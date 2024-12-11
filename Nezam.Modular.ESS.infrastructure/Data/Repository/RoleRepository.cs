using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.Users.ValueObjects;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Infrastructure.Data.Repository;

public class RoleRepository : EfCoreBonRepository<RoleEntity,IdentityDbContext>, IRoleRepository
{




    public Task<RoleEntity> GetRoleByIdAsync(RoleId roleId)
    {
        return GetOneAsync(x=>x.RoleId == roleId);
    }
}