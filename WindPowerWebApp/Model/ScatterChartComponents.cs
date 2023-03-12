using AntDesign.Charts;
using System.ComponentModel;
using System.Reflection;

namespace WindPowerWebApp.Model
{
    public class ScatterChartComponents
    {
        public PropertyInfo Prop;
        public string Title;
        //public IChartComponent ChartComponent;
        public ScatterConfig Config;

        public ScatterChartComponents(PropertyInfo prop, string against)
        {
            Prop = prop;
            Title = ((DisplayNameAttribute)prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() ?? new DisplayNameAttribute(prop.Name)).DisplayName;
            Config = new ScatterConfig
            {
                AutoFit = true,
                Padding = "auto",
                XField = against,
                YField = prop.Name.ToLower(),
                PointStyle = new GraphicStyle
                {
                    Stroke = "#777777",
                    LineWidth = 1,
                    Fill = "#5B8FF9",
                },
                RegressionLine = new RegressionLineConfig
                {
                    Type = "linear",
                }
            };
        }
    }
}
