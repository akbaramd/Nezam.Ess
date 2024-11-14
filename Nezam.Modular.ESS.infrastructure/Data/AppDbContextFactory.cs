using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
{
    public IdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

        optionsBuilder.UseSqlite($"Data Source=../Nezam.Modular.ESS.WebApi/NezamEes.db");

        var xtx =  new IdentityDbContext(optionsBuilder.Options);
        return xtx;
    }
}