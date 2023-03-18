using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    public class DataModelBase
    {
        [DisplayName("Date time")]
        public DateTime DateTime { get; set; }
        [DisplayName("Generator Voltage (V)")]
        public float? Voltage_generator { get; set; }
        [DisplayName("Generator Current (A)")]
        public float? Current_generator { get; set; }
        [DisplayName("Battery Voltage (V)")]
        public float? Voltage_battery { get; set; }
        [DisplayName("Battery Current (A)")]
        public float? Current_battery { get; set; }
        public float? RPM { get; set; }

        [DisplayName("Wind Speed (m/s)")]
        public float? WindSpeed { get; set; }
    }
}
