namespace WindPowerWebApp.Model
{
    public class DataModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public float? Voltage { get; set; }
        public float? Current { get; set; }
        public float? RPM { get; set; }
        public float? WindSpeed { get; set; }
    }
}
