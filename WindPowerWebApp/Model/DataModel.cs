using SqlSugar;
using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    [SugarTable("SystemData")]
    public class DataModel
    {
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        [DisplayName("Inverter Voltage (W)")]
        public float? Voltage_inverter { get; set; }
        [DisplayName("Inverter Current (A)")]
        public float? Current_inverter { get; set; }
        [DisplayName("Generator Voltage (W)")]
        public float? Voltage_generator { get; set; }
        [DisplayName("Generator Current (A)")]
        public float? Current_generator { get; set; }
        [DisplayName("RPM (m/s)")]
        public float? RPM { get; set; }
        [DisplayName("WindSpeed (m/s)")]
        public float? WindSpeed { get; set; }

        [DisplayName("Date time")]
        public DateTime DateTime { get; set; }

        public string NullorAddUnit(float? num, string unit)
        {
            return num == null ? "==" : num.Value.ToString("n3") + unit;
        }
    }
}
