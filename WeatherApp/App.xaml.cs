using System.Windows;
using WeatherApp.Weather.Views;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartUp(object sender, StartupEventArgs e)
        {
            //TODO: use something like structure map or w/e to make the view model and inject that into weatherview
            var weatherView = new WeatherView(new Weather.ViewModels.WeatherViewModel());
            weatherView.Show();
        }
    }
}
