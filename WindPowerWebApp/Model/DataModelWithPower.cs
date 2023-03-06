using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    public class DataModelForChart
    {
        public float? Power_generator { get; set; }
        public float? Voltage_generator { get; set; }
        public float? Current_generator { get; set; }
        public float? Power_inverter { get; set; }
        public float? Voltage_inverter { get; set; }
        public float? Current_inverter { get; set; }
        public float? RPM { get; set; }
        public float? WindSpeed { get; set; }
        public string DateTimeStr { get; set; }
        public DateTime DateTime { get; set; }

        public DataModelForChart(DataModel data)
        {
            this.DateTime = data.DateTime;
            this.DateTimeStr = this.DateTime.ToString("M/d HH:mm:ss");
            this.Current_generator = data.Current_generator;
            this.Voltage_generator = data.Voltage_generator;
            this.Current_inverter = data.Current_inverter;
            this.Voltage_inverter = data.Voltage_inverter;
            this.RPM = data.RPM;
            this.WindSpeed = data.WindSpeed;
            this.Power_generator = this.Current_generator * this.Voltage_generator;
            this.Power_inverter = this.Current_inverter * this.Voltage_inverter;
        }
    }
}
