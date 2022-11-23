using WindPowerWebApp.Model;
using System.Data.SqlClient;
using Dapper;
using SqlSugar;

namespace WindPowerWebApp.Data
{
    public class SqlDbService
    {
        private readonly IConfiguration Configuration;
        private readonly string WindPowerDbConStr;
        private SqlSugarClient sqlSugarClient;

        public SqlDbService(IConfiguration configuration)
        {
            Configuration = configuration;
            WindPowerDbConStr = Configuration.GetConnectionString("WindPowerDb");
            sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = WindPowerDbConStr,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }

        public List<DataModel> GetAllSystemData()
        {
            using (var conn = new SqlConnection(WindPowerDbConStr))
            {
                var result = conn.Query<DataModel>("SELECT * FROM [dbo].[SystemData] Order by [DateTime]").ToList();
                return result;
            }
        }

        public DataModel GetLatestSystemData()
        {
            using (var conn = new SqlConnection(WindPowerDbConStr))
            {
                var result = conn.Query<DataModel>("SELECT TOP (1) * FROM [dbo].[SystemData] Order by [DateTime] DESC").SingleOrDefault();
                return result;
            }
        }

        public void AddSystemData(List<DataModel> dataList)
        {
            sqlSugarClient.Insertable(dataList).ExecuteCommand();
        }
    }
}
