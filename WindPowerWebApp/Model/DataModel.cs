using SqlSugar;
using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    [SugarTable("SystemData")]
    public class DataModel : DataModelBase
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string NullorAddUnit(float? num, string unit)
        {
            return num == null ? "==" : num.Value.ToString("n3") + unit;
        }
    }
}
