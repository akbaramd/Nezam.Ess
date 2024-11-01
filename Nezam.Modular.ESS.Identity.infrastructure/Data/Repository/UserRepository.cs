using Bonyan.Layer.Domain;
using Bonyan.UserManagement.Domain.ValueObjects;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data.Repository;

public class UserRepository : EfCoreRepository<UserEntity, UserId, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext userManagementDbContext) : base(userManagementDbContext)
    {
    }
}