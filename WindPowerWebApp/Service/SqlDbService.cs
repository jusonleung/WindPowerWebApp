using WindPowerWebApp.Model;
using System.Data.SqlClient;
using Dapper;
using SqlSugar;
using DocumentFormat.OpenXml.Wordprocessing;
using GoogleMapsComponents.Maps;

namespace WindPowerWebApp.Service
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

        public LatLngLiteral GetLastestPosition()
        {
            var result = GetSqlSugarClient().Queryable<DataModel>()
                .OrderBy(d => d.DateTime, OrderByType.Desc)
                .Where(d => d.Latitude != null && d.Longitude != null).First();

            return new LatLngLiteral
            {
                Lat = result.Latitude.Value,
                Lng = result.Longitude.Value
            };
        }
    }
}
