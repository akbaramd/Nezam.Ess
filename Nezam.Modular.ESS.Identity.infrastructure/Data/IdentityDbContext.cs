using Bonyan.EntityFrameworkCore;
using Bonyan.Layer.Domain.Enumerations;
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
    public DbSet<UserVerificationTokenEntity> UserVerificationToken { get; set; }
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
        modelBuilder.Entity<UserVerificationTokenEntity>().ConfigureByConvention();
        modelBuilder.Entity<EngineerEntity>().ConfigureByConvention();
        modelBuilder.Entity<EmployerEntity>().ConfigureByConvention();

        modelBuilder.Entity<EmployerEntity>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        modelBuilder.Entity<EngineerEntity>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        
        modelBuilder.Entity<UserEntity>().HasMany(x => x.Roles).WithMany(c=>c.Users).UsingEntity("UserRoles");
        modelBuilder.Entity<UserEntity>().HasMany(x => x.VerificationTokens).WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<RoleEntity>().HasIndex(x => x.Name);

        modelBuilder.Entity<UserVerificationTokenEntity>().HasKey(x => x.Token);
        modelBuilder.Entity<UserVerificationTokenEntity>().Property(x => x.Type).HasConversion(c => c.Name.ToString(),
            v => Enumeration.FromName<UserVerificationTokenType>(v) ?? UserVerificationTokenType.Global);
        
        base.OnModelCreating(modelBuilder);
    }
}