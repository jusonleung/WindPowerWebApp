using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    public class DataModelWithPower : DataModel
    {
        [DisplayName("Generator Power (W)")]
        public float? Power_generator { get; set; }
        [DisplayName("Battery Power (W)")]
        public float? Power_battery { get; set; }
        public DataModelWithPower(DataModel data)
        {
            this.DateTime = data.DateTime;
            this.Current_generator = data.Current_generator;
            this.Voltage_generator = data.Voltage_generator;
            this.Current_battery = data.Current_battery;
            this.Voltage_battery = data.Voltage_battery;
            this.RPM = data.RPM;
            this.WindSpeed = data.WindSpeed;
            this.Power_generator = this.Current_generator * this.Voltage_generator ?? null;
            this.Power_battery = this.Current_battery * this.Voltage_battery ?? null;
            this.Longitude = data.Longitude;
            this.Latitude = data.Latitude;
        }
    }
}
