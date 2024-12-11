using Bonyan.EntityFrameworkCore;
using Bonyan.IdentityManagement.Domain.Permissions;
using Bonyan.IdentityManagement.Domain.Roles;
using Bonyan.IdentityManagement.Domain.Users;
using Bonyan.IdentityManagement.EntityFrameworkCore;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class IdentityDbContext : BonDbContext<IdentityDbContext>
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserVerificationTokenEntity> UserVerificationTokens { get; set; }
    public DbSet<EngineerEntity> Engineers { get; set; }
    public DbSet<EmployerEntity> Employers { get; set; }
    public DbSet<DocumentAggregateRoot> Documents { get; set; }
    public DbSet<DocumentAttachmentEntity> DocumentAttachments { get; set; }
    public DbSet<DocumentReferralEntity> DocumentReferrals { get; set; }
    public DbSet<DocumentActivityLogEntity> DocumentActivityLogs { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        ConfigureEntities(modelBuilder);
        ConfigureRelationships(modelBuilder);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserEntity>().ConfigureByConvention();
        modelBuilder.Entity<UserVerificationTokenEntity>().ConfigureByConvention();
        modelBuilder.Entity<EngineerEntity>().ConfigureByConvention();
        modelBuilder.Entity<EmployerEntity>().ConfigureByConvention();
        modelBuilder.Entity<DocumentAggregateRoot>().ConfigureByConvention();
        modelBuilder.Entity<UserVerificationTokenEntity>().ConfigureByConvention();
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<UserEntity>()
            .OwnsOne(x => x.UserName, v =>
            {
                v.Property(x=>x.Value).HasColumnName("UserName");
            });
        
        modelBuilder.Entity<UserEntity>()
            .OwnsOne(x => x.Email, v =>
            {
                v.Property(x=>x.Value).HasColumnName("Email");
            });
        
        modelBuilder.Entity<UserEntity>()
            .OwnsOne(x => x.Password, v =>
            {
                v.Property(x=>x.Value).HasColumnName("Password");
            });
        modelBuilder.Entity<UserEntity>()
            .OwnsOne(x => x.Profile, v =>
            {
                v.Property(x=>x.Avatar).HasColumnName("Avatar");
                v.Property(x=>x.FirstName).HasColumnName("FirstName");
                v.Property(x=>x.LastName).HasColumnName("LastName");
            });

        modelBuilder.Entity<UserEntity>().HasKey(x => x.UserId);
        modelBuilder.Entity<RoleEntity>().HasKey(x => x.RoleId);
        modelBuilder.Entity<EngineerEntity>()
            .HasBaseType<UserEntity>()
            .ToTable("Engineers");  // Set a custom table for EngineerEntity
    
        modelBuilder.Entity<EmployerEntity>()
            .HasBaseType<UserEntity>()
            .ToTable("Employers");  // Set a custom table for EmployerEntity

        // If EngineerEntity has a separate key, configure it here:
        // If EngineerEntity has a separate key, configure it here:
        modelBuilder.Entity<EngineerEntity>()
            .Property(e => e.EngineerId)
            .HasColumnName("EngineerId");

        modelBuilder.Entity<EmployerEntity>()
            .Property(e => e.EmployerId)
            .HasColumnName("EmployerId");
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.VerificationTokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<DocumentAggregateRoot>()
            .HasMany(d => d.Attachments)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);
        
        modelBuilder.Entity<DocumentAggregateRoot>()
            .HasMany(d => d.Referrals)
            .WithOne()
            .HasForeignKey(r => r.DocumentId);
        
        
        modelBuilder.Entity<DocumentAggregateRoot>()
            .HasMany(d => d.ActivityLogs)
            .WithOne()
            .HasForeignKey(a => a.DocumentId);
        
        modelBuilder.Entity<DocumentAggregateRoot>()
            .HasOne(d => d.OwnerUser)
            .WithMany()
            .HasForeignKey(a => a.OwnerUserId);
    }


    public DbSet<BonIdentityUserToken> UserTokens { get; set; }
    public DbSet<BonIdentityRole> Roles { get; set; }
    public DbSet<BonIdentityPermission> Permissions { get; set; }
}
