namespace WindPowerWebApp.Model
{
    public class RpiStatusModel
    {
        public bool? Online { get; set; }
        public DataModel? SystemData { get; set; }
        public bool? Sending { get; set; }
        public float? Interval { get; set; }
    }
}
