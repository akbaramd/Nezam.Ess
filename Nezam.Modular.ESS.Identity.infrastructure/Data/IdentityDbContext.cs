using Bonyan.EntityFrameworkCore;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data;

public class IdentityDbContext :BonyanDbContext<IdentityDbContext>, IBonUserManagementDbContext<UserEntity>
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<EngineerEntity> Engineers { get; set; }
    public DbSet<EmployerEntity> Employers { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUserManagementByConvention<UserEntity>();
        modelBuilder.Entity<RoleEntity>().ConfigureByConvention();
        modelBuilder.Entity<EngineerEntity>().ConfigureByConvention();
        modelBuilder.Entity<EmployerEntity>().ConfigureByConvention();

        modelBuilder.Entity<EmployerEntity>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        modelBuilder.Entity<EngineerEntity>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<UserEntity>().HasMany(x => x.Roles).WithMany().UsingEntity("UserRoles");

        modelBuilder.Entity<RoleEntity>().HasIndex(x => x.Name);
        
        base.OnModelCreating(modelBuilder);
    }
}