using Dapper;
using Dot.Net.Core.Common.DTO;
using Dot.Net.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dot.Net.Infrastructure.Data
{
    public class PalindromeRepository : IPalidromeRepository
    {
        private readonly IConnectToDatabase _connectionManager;

        public PalindromeRepository(IConnectToDatabase connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public bool Delete(PalindromeDTO palindrome)
        {
            throw new NotImplementedException();
        }

        public List<PalindromeDTO> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (SqlConnection con = _connectionManager.GetConnection())
            {
                con.Open();
                var result = con.Query<PalindromeDTO>("uspGetAllPalindrome", param: parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public bool Save(PalindromeDTO palindrome)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@palindromeName", palindrome.Name, DbType.String);
            using (SqlConnection con = _connectionManager.GetConnection())
            {
                con.Open();
                var result = con.Query<bool>("uspSavePalindrome", param: parameters, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public bool Update(PalindromeDTO palindrome)
        {
            throw new NotImplementedException();
        }
    }
}
