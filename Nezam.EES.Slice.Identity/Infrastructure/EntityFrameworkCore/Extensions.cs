using Microsoft.EntityFrameworkCore;
using Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore.Configurations;

namespace Nezam.EES.Service.Identity.Infrastructure.EntityFrameworkCore;

public static class Extensions
{
    /// <summary>
    /// Configures the DbContext with specific configurations for the Identity slice.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void ApplyIdentityConfigurations(this ModelBuilder modelBuilder)
    {
        // Example: Apply configuration classes in the Identity slice
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenEntityConfiguration());
        
        // Add additional configurations here
    }
}