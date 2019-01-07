using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApiProxy.App.Weather.Contracts;
using WeatherApp.Weather.Models;

namespace WeatherApp.Weather.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string _address;
        public string Address { get => _address; set => _address = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Forecast _forecast;
        public Forecast Forecast
        {
            get => _forecast;
            set {
                _forecast = value;
                OnPropertyChanged("Forecast");
            }
        }

        public WeatherViewModel()
        {
            
        }

        internal async Task GetWeather()
        {
            if (string.IsNullOrWhiteSpace(Address)) return;

            var logic = new WeatherLogic();
            
            Forecast = await logic.GetForecast(Address);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
