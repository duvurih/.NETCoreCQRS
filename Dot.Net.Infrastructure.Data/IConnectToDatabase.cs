using System.Data.SqlClient;

namespace Dot.Net.Infrastructure.Data
{
    public interface IConnectToDatabase
    {
        SqlConnection GetConnection();
    }
}
