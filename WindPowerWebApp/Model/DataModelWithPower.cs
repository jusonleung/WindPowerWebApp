using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    public class DataModelWithPower
    {
        [DisplayName("Date time")]
        public DateTime DateTime { get; set; }

        [DisplayName("Generator Voltage (V)")]
        public float? Voltage_generator { get; set; }

        [DisplayName("Generator Current (A)")]
        public float? Current_generator { get; set; }

        [DisplayName("Generator Power (W)")]
        public float? Power_generator { get; set; }

        [DisplayName("Inverter Voltage (V)")]
        public float? Voltage_inverter { get; set; }

        [DisplayName("Inverter Current (A)")]
        public float? Current_inverter { get; set; }

        [DisplayName("Inverter Power (W)")]
        public float? Power_inverter { get; set; }

        public float? RPM { get; set; }

        [DisplayName("Wind Speed (m/s)")]
        public float? WindSpeed { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }

        public DataModelWithPower(DataModel data)
        {
            this.DateTime = data.DateTime;
            this.Current_generator = data.Current_generator;
            this.Voltage_generator = data.Voltage_generator;
            this.Current_inverter = data.Current_inverter;
            this.Voltage_inverter = data.Voltage_inverter;
            this.RPM = data.RPM;
            this.WindSpeed = data.WindSpeed;
            this.Power_generator = this.Current_generator * this.Voltage_generator ?? null;
            this.Power_inverter = this.Current_inverter * this.Voltage_inverter ?? null;
            this.Longitude = data.Longitude;
            this.Latitude = data.Latitude;
        }
    }
}
