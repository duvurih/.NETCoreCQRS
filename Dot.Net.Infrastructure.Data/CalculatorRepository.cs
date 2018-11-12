using Dapper;
using Dot.Net.Core.Common.Enums;
using Dot.Net.Core.Interfaces.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dot.Net.Infrastructure.Data
{
    public class CalculatorRepository : ICalculatorRepository
    {
        private readonly IConnectToDatabase _connectionManager;

        public CalculatorRepository(IConnectToDatabase connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public int Calculate(GlobalEnums.OperatorsEnum operatorValue, int resultValue)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@operator", operatorValue, DbType.Int32);
            parameters.Add("@result", resultValue, DbType.Int32);
            using (SqlConnection con = _connectionManager.GetConnection())
            {
                con.Open();
                var result = con.Query<int>("uspStoreCalculatorResult", param: parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
    }
}
