using System;

namespace WeatherApp.Weather.Models
{
    public class DataPoint
    {
        public DateTimeOffset Time { get; set; }
        public string Summary { get; set; }
        public double? Temperature { get; set; }
        public double? TemperatureLow { get; set; }
        public double? TemperatureHigh { get; set; }
    }
}
