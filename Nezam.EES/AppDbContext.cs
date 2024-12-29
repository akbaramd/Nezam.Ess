using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;
using Nezam.EES.Slice.Secretariat.Domains.Documents;
using Nezam.EES.Slice.Secretariat.Domains.Participant;
using Nezam.EES.Slice.Secretariat.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Gateway;

public class AppDbContext : DbContext , IIdentityDbContext , ISecretariatDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyIdentityConfigurations();
        modelBuilder.ApplySecretariatConfigurations();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DocumentAggregateRoot> Documents { get; set; }
    public DbSet<DocumentAttachmentEntity> DocumentAttachments { get; set; }
    public DbSet<DocumentReferralEntity> DocumentReferrals { get; set; }
    public DbSet<Participant> Participants { get; set; }
}