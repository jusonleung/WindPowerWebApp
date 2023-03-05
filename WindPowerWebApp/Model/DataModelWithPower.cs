using System.ComponentModel;

namespace WindPowerWebApp.Model
{
    public class DataModelForChart
    {
        public float? Power { get; set; }
        public float? Voltage { get; set; }
        public float? Current { get; set; }
        public float? RPM { get; set; }
        public float? WindSpeed { get; set; }
        public string DateTimeStr { get; set; }
        public DateTime DateTime { get; set; }

        public DataModelForChart(DataModel data)
        {
            this.DateTime = data.DateTime;
            this.DateTimeStr = this.DateTime.ToString("M/d HH:mm:ss");
            this.Current = data.Current;
            this.Voltage = data.Voltage;
            this.RPM = data.RPM;
            this.WindSpeed = data.WindSpeed;
            this.Power = this.Current * this.Voltage;
        }
    }
}
