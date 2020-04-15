using App_TelaClima.Models;
using System.Net.Http;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;

namespace App_TelaClima.Services
{
    public enum Units
    {
        Imperial,
        Metric
    }

    public class WeatherService
    {
        const string WeatherCityUri = "https://api.openweathermap.org/data/2.5/weather?q={0}&units={1}&appid=15ed43585f07a7f56a9374222a191d88";
        const string ForecaseUri = "https://api.openweathermap.org/data/2.5/forecast?id={0}&units={1}&appid=15ed43585f07a7f56a9374222a191d88";

        public async Task<WeatherRoot> GetWeather(string city, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(WeatherCityUri, city, units.ToString().ToLower());
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherRoot>(json);
            }

        }

        public async Task<WeatherForecastRoot> GetForecast(int id, Units units = Units.Imperial)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecaseUri, id, units.ToString().ToLower());
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }

        }
    }
}
