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
        public string TestString { get => "Hello World!!!!!"; }

        public string Address { get => _address; set => _address = value; }
        private string _address;

        public event PropertyChangedEventHandler PropertyChanged;

        private Forecast _test;
        public Forecast Test
        {
            get => _test;
            set {
                _test = value;
                OnPropertyChanged("Test");
            }
        }

        public WeatherViewModel()
        {
            
        }

        internal async Task GetWeather()
        {
            var logic = new WeatherLogic();
            
            Test = await logic.GetForecast(Address);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
