using Bonyan.EntityFrameworkCore;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data;

public class IdentityDbContext :BonyanDbContext<IdentityDbContext>, IBonUserManagementDbContext<UserEntity>
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUserManagementByConvention<UserEntity>();
        modelBuilder.Entity<UserEntity>().HasMany(x => x.Users).WithMany(x => x.Users).UsingEntity("UserRoles");

        modelBuilder.Entity<RoleEntity>().ConfigureByConvention();
        modelBuilder.Entity<RoleEntity>().HasIndex(x => x.Name);
        base.OnModelCreating(modelBuilder);
    }
}