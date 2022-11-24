using WindPowerWebApp.Model;
using System.Data.SqlClient;
using Dapper;
using SqlSugar;

namespace WindPowerWebApp.Data
{
    public class SqlDbService
    {
        private SqlSugarClient sqlSugarClient;

        public SqlDbService(IConfiguration configuration)
        {
            sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = configuration.GetConnectionString("WindPowerDb"),
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }

        public List<DataModel> GetAllSystemData()
        {
            return sqlSugarClient.Queryable<DataModel>().ToList();
        }

        public DataModel GetLatestSystemData()
        {
            return sqlSugarClient.Queryable<DataModel>().OrderBy(d => d.DateTime, OrderByType.Desc).First();
        }

        public void AddSystemData(List<DataModel> dataList)
        {
            dataList.ForEach(d => { if (d.DateTime > DateTime.Now) dataList.Remove(d); });
            sqlSugarClient.Insertable(dataList).ExecuteCommand();
        }
    }
}
