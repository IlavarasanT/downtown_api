using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.IdentityModel.Tokens;

namespace downtown.Services
{
    //public class SqlService { }
    public class SqlService
    {
        #region  Variable Declaration

        private IConfiguration _configuration;
        public static string GetConnectionString(string key)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return config["ConnectionString:" + key] != null ? config["ConnectionString:" + key] : string.Empty;
        }
        private IDbConnection dbConnection => new SqlConnection(GetConnectionString("dbConnectionStrings"));

        #endregion

        public SqlService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Query 

        public async Task<T> GetSingleExecuteQueryasync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var dbConn = dbConnection)
            {
                return await dbConn.QueryFirstOrDefaultAsync<T>(query, param, commandType: commandType);
            }

        }
        public async Task<T> GetSingleExecuteSPasync<T>(string storedProcedure, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var dbConn = dbConnection)
            {
                return await dbConn.QueryFirstOrDefaultAsync<T>(storedProcedure, param, commandType: commandType);
            }

        }

        public async Task<List<T>> GetListExecuteQueryasync<T>(string query, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var dbConn = dbConnection)
            {
                var result = await dbConn.QueryAsync<T>(query, param, commandType: CommandType.Text);

                return result.ToList();
            }
        }
        #endregion

        #region Stored Procedure
        public async Task<int> ExecuteSP(string storedProcedure, object parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var dbConn = dbConnection)
            {
                int result = await dbConn.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<T>> SPGetListExecuteQueryasync<T>(string storedProcedure, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {

            using (var dbConn = dbConnection)
            {
                var result = await dbConn.QueryAsync<T>(storedProcedure, param, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        #endregion


    }
}
