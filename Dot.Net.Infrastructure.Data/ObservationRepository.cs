using Dapper;
using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Common.Extensions;
using Dot.Net.Core.Interfaces.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Net.Infrastructure.Data
{
    public class ObservationRepository : IObservationRepository
    {
        private readonly IConnectToDatabase _connectionManager;

        public ObservationRepository(IConnectToDatabase connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task<IEnumerable<ObservationDTO>> GetObservations(string userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@recordedBy", userId, DbType.String);
            using (SqlConnection con = _connectionManager.GetConnection())
            {
                con.Open();
                var result = await con.QueryAsync<ObservationDTO>("uspGetObservations", param: parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<bool> Save(ObservationDTO observation)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@observation", observation.CreateXML(), DbType.Xml);
            using (SqlConnection con = _connectionManager.GetConnection())
            {
                con.Open();
                var result = await con.QueryAsync<bool>("uspSaveObservation", param: parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
    }
}
