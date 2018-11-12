using Dot.Net.Core.Common.Settings;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Dot.Net.Infrastructure.Data
{
    public class ConnectDB : IConnectToDatabase
    {
        IOptions<DatabaseSettingsConfig> _dbOptions;

        public ConnectDB(IOptions<DatabaseSettingsConfig> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._dbOptions.Value.ConnectionString);
        }
    }
}
