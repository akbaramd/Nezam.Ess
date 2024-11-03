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
        // optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ConfigureUserManagementByConvention<UserEntity>();
        modelBuilder.Entity<RoleEntity>().ConfigureByConvention();
        modelBuilder.Entity<UserVerificationTokenEntity>().ConfigureByConvention();
        modelBuilder.Entity<EngineerEntity>().ConfigureByConvention();
        modelBuilder.Entity<EmployerEntity>().ConfigureByConvention();

        modelBuilder.Entity<EngineerEntity>()
            .HasOne(e => e.User)
            .WithOne(c=>c.Engineer) // No navigation property on UserEntity side
            .HasForeignKey<EngineerEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<EmployerEntity>()
            .HasOne(x => x.User)
            .WithOne(c=>c.Employer) // No navigation property on UserEntity side
            .HasForeignKey<EmployerEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
        
        modelBuilder.Entity<UserEntity>().HasMany(x => x.Roles).WithMany(c=>c.Users).UsingEntity("UserRoles");
        modelBuilder.Entity<UserEntity>().HasMany(x => x.VerificationTokens).WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<RoleEntity>().HasIndex(x => x.Name);
        modelBuilder.Ignore<UserVerificationTokenType>();   
        modelBuilder.Entity<UserVerificationTokenEntity>()
            .Property(x => x.Type)
            .HasConversion<string>(
                c => c.Name,
                v =>  UserVerificationTokenType.Global
            );

    }
}