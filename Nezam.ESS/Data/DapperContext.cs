using System.Data;
using Microsoft.Data.SqlClient;

namespace Nezam.ESS.backend.Data;

public class DapperContext(IConfiguration configuration)
{
    public IDbConnection CreateConnection()
    {
        return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
    // => new SqlConnection("Data Source=85.185.6.4;Initial Catalog=map2;User ID=new_site_user;Password=111qqqAAAn;Trust Server Certificate=True;");
}