using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Users;
using Payeh.SharedKernel.EntityFrameworkCore;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;

public interface IIdentityDbContext : IPayehDbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
}