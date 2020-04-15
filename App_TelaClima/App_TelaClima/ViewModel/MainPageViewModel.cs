using App_TelaClima.Models;
using App_TelaClima.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace App_TelaClima.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public static WeatherService WeatherService { get; } = new WeatherService();
        public static WeatherRoot weatherRoot { get; set; }
        public static WeatherForecastRoot forecast { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string _Nuvem { get; set; }
        public string Nuvem {get { return _Nuvem; } set { _Nuvem = value; OnPropertyChanged("Nuvem"); } }
        public string _VelocidadeV { get; set; }
        public string VelocidadeV { get { return _VelocidadeV; } set { _VelocidadeV = value; OnPropertyChanged("VelocidadeV"); } }
        public string _Umidade { get; set; }
        public string Umidade { get { return _Umidade; } set { _Umidade = value; OnPropertyChanged("Umidade"); } }
        public string _Temp { get; set; }
        public string Temp { get { return _Temp; } set { _Temp = value; OnPropertyChanged("Temp"); } }
        public string _TempMax { get; set; }
        public string TempMax { get { return _TempMax; } set { _TempMax = value; OnPropertyChanged("TempMax"); } }
        public string _TempMin { get; set; }
        public string TempMin { get { return _TempMin; } set { _TempMin = value; OnPropertyChanged("TempMin"); } }
        public string _Endereco { get; set; }
        public string Endereco { get { return _Endereco; } set { _Endereco = value; OnPropertyChanged("Endereco"); } }
        public string _Clima { get; set; }
        public string Clima { get { return _Clima; } set { _Clima = value; OnPropertyChanged("Clima"); } }
        public MainPageViewModel()
        {
            ExecuteGetWeather();
        }

        public async Task ExecuteGetWeather()
        {
            try
            {
                //Get weather by city
                weatherRoot = await WeatherService.GetWeather("São Paulo", Units.Metric);
                //Get forecast based on cityId
                forecast = await WeatherService.GetForecast(weatherRoot.CityId, Units.Metric);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                Nuvem = weatherRoot.Clouds.CloudinessPercent.ToString() + "%";
                VelocidadeV = weatherRoot.Wind.Speed.ToString() + "m/s";
                Umidade = weatherRoot.MainWeather.Humidity.ToString() + "%";
                Temp = ((int)weatherRoot.MainWeather.Temperature).ToString();
                TempMax = ((int)weatherRoot.MainWeather.MaxTemperature).ToString() + "°C";
                TempMin = ((int)weatherRoot.MainWeather.MinTemperature).ToString() + "°C";
                Endereco = weatherRoot.Name.ToString() + ", "+ weatherRoot.System.Country;
                Clima = weatherRoot.Weather.ElementAt(0).Description;
            }
        }
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }
}
