using SqlSugar;
using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    [SugarTable("SystemData")]
    public class DataModel
    {
        public DateTime DateTime { get; set; }
        public float? Voltage_generator { get; set; }
        public float? Current_generator { get; set; }
        public float? Voltage_inverter { get; set; }
        public float? Current_inverter { get; set; }
        public float? RPM { get; set; }
        public float? WindSpeed { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string NullorAddUnit(float? num, string unit)
        {
            return num == null ? "==" : num.Value.ToString("n3") + unit;
        }
    }
}
