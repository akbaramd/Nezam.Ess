using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Domains.Roles;
using Nezam.EES.Service.Identity.Domains.Users;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;

namespace Nezam.EES.Gateway;

public class AppDbContext : DbContext , IIdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyIdentityConfigurations();
        base.OnModelCreating(modelBuilder);
    }
}