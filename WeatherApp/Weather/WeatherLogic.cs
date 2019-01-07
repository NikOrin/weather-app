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
        public async Task<(Forecast, string)> GetForecast(string address)
        {
            var proxy = new WeatherProxy(ConfigurationManager.AppSettings["WeatherApiBaseUri"].TrimEnd('/'));
            var response = await proxy.GetForecast(address);

            if (!response.IsSuccess) return (null, response.ErrorMessage);

            return (BuildForecastModel(response.Data), null);
        }

        private Forecast BuildForecastModel(ApiForecast apiForecast)
        {
            var forecast = new Forecast();

            forecast.Today = BuildDataPointModel(apiForecast.Current);
            var today = apiForecast.Daily.First();
            forecast.Today.TemperatureHigh = today.TemperatureHigh;
            forecast.Today.TemperatureLow = today.TemperatureLow;
            forecast.Today.ChanceOfRain = today.PrecipitationProbability.GetValueOrDefault() * 100;

            forecast.Hourly = apiForecast.Hourly.Select(x => BuildDataPointModel(x));
            forecast.Daily = apiForecast.Daily.Select(x => BuildDataPointModel(x));

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
                TemperatureLow = apiDataPoint.TemperatureLow,
                TemperatureFeelsLike = apiDataPoint.TemperatureFeelsLike,
                ChanceOfRain = apiDataPoint.PrecipitationProbability.GetValueOrDefault() * 100
            };
        }
    }
}
