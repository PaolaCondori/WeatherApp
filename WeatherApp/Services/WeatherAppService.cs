using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;
namespace WeatherApp.Services
{
    public class WeatherAppService
    {
            private HttpClient httpClient;
            private WeatherModel weatherModel;
            private JsonSerializerOptions jsonSerializerOptions;
            private string url = $"https://api.openweathermap.org/data/2.5/weather?";
            private Uri uri;


        public WeatherAppService()
            {
                httpClient = new HttpClient();
                jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
            }

            public async Task<WeatherModel> GetWeatherAsync(string cityName, string Country) 
            {/*string countryCode*/


            try
            {
                uri = new Uri(url + $"q={cityName},{Country}" + $"&appid=1a1f2150d9d9658cbc6e07b8efc9c9ee&lang=pt_br&units=metric");
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(content);
                        weatherModel = JsonSerializer.Deserialize<WeatherModel>(content, jsonSerializerOptions);
                    }
                }
                catch
                {
                    /*Fazer o cacth Tratar os erros, cidade não existe, valor nulo
                     verificar se os que foi passado por param é string
                    verificar*/
                }
               return weatherModel; /*tá nulo*/
            }


        }
}
