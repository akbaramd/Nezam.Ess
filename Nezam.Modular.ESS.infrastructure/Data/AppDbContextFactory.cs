using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlite($"EntityFrameworkCore Source=../Nezam.Modular.ESS.WebApi/NezamEes.db");

        var xtx =  new AppDbContext(optionsBuilder.Options);
        return xtx;
    }
}