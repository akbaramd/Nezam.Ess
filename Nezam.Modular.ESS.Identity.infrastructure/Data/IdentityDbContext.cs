using Bonyan.EntityFrameworkCore;
using Bonyan.UserManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.infrastructure.Data;

public class IdentityDbContext :BonyanDbContext<IdentityDbContext>, IBonUserManagementDbContext<UserEntity>
{
    public DbSet<UserEntity> Users { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureUserManagementByConvention<UserEntity>();
        base.OnModelCreating(modelBuilder);
    }
}