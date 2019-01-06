using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather;
using System.Configuration;
using WeatherApiProxy.App.Weather.Contracts;
using WeatherApp.Weather.Models;
using ApiForecast = WeatherApiProxy.App.Weather.Models.Forecast;
using ApiDataPoint = WeatherApiProxy.App.Weather.Models.DataPoint;

namespace WeatherApp.Weather
{
    public class WeatherLogic
    {
        public async Task<Forecast> GetForecast(string address)
        {
            var proxy = new WeatherProxy(ConfigurationManager.AppSettings["WeatherApiBaseUri"].TrimEnd('/'));
            var response = await proxy.GetForecast(address);

            if (!response.IsSuccess) return null;

            return BuildForecastModel(response.Data);
        }

        private Forecast BuildForecastModel(ApiForecast apiForecast)
        {
            var forecast = new Forecast();

            forecast.Current = BuildDataPointModel(apiForecast.Current);

            return forecast;
        }

        private DataPoint BuildDataPointModel(ApiDataPoint apiDataPoint)
        {
            return new DataPoint
            {
                Time = apiDataPoint.Time,
                Summary = apiDataPoint.Summary,
                Temperature = apiDataPoint.Temperature,
                TemperatureHigh = apiDataPoint.TemperatureHigh,
                TemperatureLow = apiDataPoint.TemperatureLow
            };
        }
    }
}
