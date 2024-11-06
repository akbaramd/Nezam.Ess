using Bonyan.EntityFrameworkCore;
using Bonyan.Layer.Domain.Enumerations;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.Employer;
using Nezam.Modular.ESS.Identity.Domain.Engineer;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.User;
using Nezam.Modular.ESS.Secretariat.Domain.Documents;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.Enumerations;
using Nezam.Modular.ESS.Secretariat.Domain.Documents.ValueObjects;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class IdentityDbContext : BonyanDbContext<IdentityDbContext>, IBonUserManagementDbContext<UserEntity>
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
        ConfigureEnumerations(modelBuilder);
    }

    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUserManagementByConvention<UserEntity>();
        
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
        modelBuilder.Entity<EngineerEntity>()
            .HasOne(e => e.User)
            .WithOne(c => c.Engineer)
            .HasForeignKey<EngineerEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        modelBuilder.Entity<EmployerEntity>()
            .HasOne(e => e.User)
            .WithOne(c => c.Employer)
            .HasForeignKey<EmployerEntity>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity("UserRoles");
        
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

    private static void ConfigureEnumerations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserVerificationTokenEntity>()
            .Property(x => x.Type)
            .HasConversion<string>(
                c => c.Name,
                v => Enumeration.FromName<UserVerificationTokenType>(v) ?? UserVerificationTokenType.Global
            );

        modelBuilder.Entity<DocumentAggregateRoot>()
            .Property(d => d.Type)
            .HasConversion<string>(
                c => c.Name,
                v => Enumeration.FromName<DocumentType>(v) ?? DocumentType.Internal
            );

        modelBuilder.Entity<DocumentAggregateRoot>()
            .Property(d => d.Status)
            .HasConversion<string>(
                c => c.Name,
                v => Enumeration.FromName<DocumentStatus>(v) ?? DocumentStatus.Draft
            );

        modelBuilder.Entity<DocumentReferralEntity>()
            .Property(r => r.Status)
            .HasConversion<string>(
                c => c.Name,
                v => Enumeration.FromName<ReferralStatus>(v) ?? ReferralStatus.Pending
            );

        modelBuilder.Entity<RoleEntity>().HasIndex(r => r.Name);
        modelBuilder.Ignore<UserVerificationTokenType>();
    }
}
