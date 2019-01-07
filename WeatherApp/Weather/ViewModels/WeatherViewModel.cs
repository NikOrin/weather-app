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
        #region Properties
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        #endregion

        public WeatherViewModel()
        {
            IsLoading = false;
        }

        internal async Task GetWeather()
        {
            SetLoading();

            if (string.IsNullOrWhiteSpace(Address)) return;

            var logic = new WeatherLogic();

            (Forecast, ErrorMessage) = await logic.GetForecast(Address);
            IsLoading = false;
        }

        private void SetLoading()
        {
            IsLoading = true;
            ErrorMessage = null;
            Forecast = null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
