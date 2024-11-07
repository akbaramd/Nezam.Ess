using Bonyan.EntityFrameworkCore;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.IdEntity.Domain.Employer;
using Nezam.Modular.ESS.IdEntity.Domain.Engineer;
using Nezam.Modular.ESS.IdEntity.Domain.Roles;
using Nezam.Modular.ESS.IdEntity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.BonEnumerations;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class IdEntityDbContext : BonDbContext<IdEntityDbContext>, IBonUserManagementDbContext<UserEntity>
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

    public IdEntityDbContext(DbContextOptions<IdEntityDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        ConfigureEntities(modelBuilder);
        ConfigureRelationships(modelBuilder);
        ConfigureBonEnumerations(modelBuilder);
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

        
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity("UserRoles");
        
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

        modelBuilder.Entity<RoleEntity>().HasIndex(r => r.Name);
        modelBuilder.Ignore<UserVerificationTokenType>();
    }
}
