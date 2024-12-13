using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nezam.Modular.ESS.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer($"data source =192.168.200.7\\\\SQL2019;initial catalog=Nezam.EES;persist security info=True;user id=sa;password=vhdSAM@15114;TrustServerCertificate=True");

        var xtx =  new AppDbContext(optionsBuilder.Options);
        return xtx;
    }
}