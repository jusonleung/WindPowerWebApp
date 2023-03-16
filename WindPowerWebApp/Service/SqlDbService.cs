using WindPowerWebApp.Model;
using SqlSugar;
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

        public byte[] GetPasswordHash(string username)
        {
            var user = GetSqlSugarClient().Queryable<UserModel>().Where(u => u.Username == username).Single();
            if (user == null)
                return null;
            return user.PasswordHash;
        }

        public List<DataModel> GetAllSystemData()
        {
            return GetSqlSugarClient().Queryable<DataModel>().OrderBy(d => d.DateTime).ToList();
        }

        public List<DataModel> GetAllSystemData(DateTime startTime, DateTime endTime)
        {
            return GetSqlSugarClient().Queryable<DataModel>()
                .Where(d => d.DateTime >= startTime && d.DateTime <= endTime)
                .OrderBy(d => d.DateTime)
                .ToList();
        }


        public DataModel GetLatestSystemData()
        {
            return GetSqlSugarClient().Queryable<DataModel>().OrderBy(d => d.DateTime, OrderByType.Desc).First();
        }

        public void AddSystemData(DataModel data)
        {
            GetSqlSugarClient().Insertable(data).ExecuteCommand();
        }

        public void AddSystemData(List<DataModel> data)
        {
            GetSqlSugarClient().Insertable(data).ExecuteCommand();
        }

        public LatLngLiteral GetLastestPosition()
        {
            var result = GetSqlSugarClient().Queryable<DataModel>()
                .OrderBy(d => d.DateTime, OrderByType.Desc)
                .Where(d => d.Latitude != null && d.Longitude != null).First();
            if (result == null)
                return null;

            return new LatLngLiteral
            {
                Lat = result.Latitude.GetValueOrDefault(),
                Lng = result.Longitude.GetValueOrDefault()
            };
        }
    }
}
