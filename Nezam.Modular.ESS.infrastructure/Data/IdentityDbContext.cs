using Bonyan.EntityFrameworkCore;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Shared.Documents.Enumerations;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class IdentityDbContext : BonDbContext<IdentityDbContext>, IBonUserManagementDbContext<UserEntity>
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserVerificationTokenEntity> UserVerificationTokens { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
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
        ConfigureBonEnumerations(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureBonUserManagementByConvention<UserEntity>();
        
        modelBuilder.Entity<RoleEntity>().ConfigureByConvention();
        modelBuilder.Entity<UserVerificationTokenEntity>().ConfigureByConvention();
        modelBuilder.Entity<EngineerEntity>().ConfigureByConvention();
        modelBuilder.Entity<EmployerEntity>().ConfigureByConvention();
        modelBuilder.Entity<DocumentAggregateRoot>().ConfigureByConvention();
        modelBuilder.Entity<DocumentAttachmentEntity>().ConfigureByConvention();
        modelBuilder.Entity<DocumentReferralEntity>().ConfigureByConvention();
        modelBuilder.Entity<DocumentActivityLogEntity>().ConfigureByConvention();
    }

    private static void ConfigureRelationships(ModelBuilder modelBuilder)
    {
                
        
        // پیکربندی RoleEntity
        modelBuilder.Entity<RoleEntity>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<RoleEntity>()
            .Property(r => r.Id)
            .HasConversion(
                roleId => roleId.Name, // برای ذخیره در پایگاه داده
                value => new RoleId(value) // برای بارگذاری از پایگاه داده
            );
        
            // پیکربندی رابطه چند به چند UserEntity و Roles
            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.Roles).WithOne()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRoleEntity>()
                .ToTable("UserRoles")
                .HasKey(x => new { x.UserId, x.RoleId });
            
            modelBuilder.Entity<UserRoleEntity>()
                .Property(r => r.RoleId)
                .HasConversion(
                    roleId => roleId.Name, // برای ذخیره در پایگاه داده
                    value => new RoleId(value) // برای بارگذاری از پایگاه داده
                );
            
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Employer)
            .WithOne(c=>c.BonUser)
            .HasForeignKey<EmployerEntity>(t => t.BonUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Engineer)
            .WithOne(u=>u.BonUser)
            .HasForeignKey<EngineerEntity>(t => t.BonUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.VerificationTokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.BonUserId);

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
            .HasForeignKey(a => a.OwnerBonUserId);
    }

    private static void ConfigureBonEnumerations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserVerificationTokenEntity>()
            .Property(x => x.Type)
            .HasConversion<string>(
                c => c.Name,
                v => BonEnumeration.FromName<UserVerificationTokenType>(v) ?? UserVerificationTokenType.Global
            );

        modelBuilder.Entity<DocumentAggregateRoot>()
            .Property(d => d.Type)
            .HasConversion<string>(
                c => c.Name,
                v => BonEnumeration.FromName<DocumentType>(v) ?? DocumentType.Internal
            );

        modelBuilder.Entity<DocumentAggregateRoot>()
            .Property(d => d.Status)
            .HasConversion<string>(
                c => c.Name,
                v => BonEnumeration.FromName<DocumentStatus>(v) ?? DocumentStatus.Draft
            );

        modelBuilder.Entity<DocumentReferralEntity>()
            .Property(r => r.Status)
            .HasConversion<string>(
                c => c.Name,
                v => BonEnumeration.FromName<ReferralStatus>(v) ?? ReferralStatus.Pending
            );

        modelBuilder.Ignore<UserVerificationTokenType>();
    }
}
