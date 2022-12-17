using SqlSugar;
using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    [SugarTable("SystemData")]
    public class DataModel
    {

        [DisplayName("Voltage (W)")]
        public float? Voltage { get; set; }
        [DisplayName("Current (A)")]
        public float? Current { get; set; }
        [DisplayName("RPM (m/s)")]
        public float? RPM { get; set; }
        [DisplayName("WindSpeed (m/s)")]
        public float? WindSpeed { get; set; }

        [DisplayName("Date time")]
        public DateTime DateTime { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public string NullorAddUnit(float? num, string unit)
        {
            return num == null ? "==" : num.Value.ToString("n3") + unit;
        }

    }
}
