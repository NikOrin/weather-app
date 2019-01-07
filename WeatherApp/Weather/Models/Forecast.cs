using System.Collections.Generic;

namespace WeatherApp.Weather.Models
{
    public class Forecast
    {
        public DataPoint Current { get; set; }
        public DataPoint Today { get; set; }
        public IEnumerable<DataPoint> Hourly { get; set; }
        public IEnumerable<DataPoint> Daily { get; set; }
    }
}
