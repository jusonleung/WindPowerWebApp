using WindPowerWebApp.Model;
using System.Data.SqlClient;
using Dapper;
using SqlSugar;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WindPowerWebApp.Data
{
    public class SqlDbService
    {
        string sqlConnectionString;

        public SqlDbService(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("WindPowerDb");
        }

        SqlSugarClient GetSqlSugarClient()
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = sqlConnectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }

        public List<DataModel> GetAllSystemData()
        {
            return GetSqlSugarClient().Queryable<DataModel>().ToList();
        }

        public DataModel GetLatestSystemData()
        {
            return GetSqlSugarClient().Queryable<DataModel>().OrderBy(d => d.DateTime, OrderByType.Desc).First();
        }

        public void AddSystemData(DataModel data)
        {
            GetSqlSugarClient().Insertable(data).ExecuteCommand();
        }
    }
}
