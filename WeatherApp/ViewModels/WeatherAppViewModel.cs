using CommunityToolkit.Mvvm.ComponentModel;
/*using Foundation;*/
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public partial class WeatherAppViewModel : ObservableObject
    {
        [ObservableProperty]
        WeatherModel weatherModel;

        [ObservableProperty]
        string country;

        [ObservableProperty]
        string cityName;

        [ObservableProperty]
        string cidadePais;

        [ObservableProperty]
        string icon;

        [ObservableProperty]
        string imageUrl;

        [ObservableProperty]
        ImageSource weatherImage;

        [ObservableProperty]
        double temp;

        [ObservableProperty]
        double tempMin;

        [ObservableProperty]
        double tempMax;

        [ObservableProperty]
        double feelsLike;

        [ObservableProperty]
        int visibility;

        [ObservableProperty]
        int humidity;

        [ObservableProperty]
        double velocity;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string localTimezoneFormat;

        WeatherAppService weatherAppService;
        
        public ICommand GetWeatherCityCommand { get; } /*Criando comando*/
        

        public WeatherAppViewModel()
        {
            GetWeatherCityCommand = new Command(GetCityWeather); /*Atribuindo o metodo getWeather ao comando*/
            weatherAppService = new WeatherAppService(); /*Criando um Service*/
        }

        public async void GetWeather(WeatherModel weatherModel) /*requisição get*/
        {
            WeatherAppService weatherAppService = new WeatherAppService();/*Criando um novo WeatherService*/
           

            /*vinculando os atributos com as classes classes*/

            
            if (WeatherModel != null)
            {
                CityName = WeatherModel.Name;
                Country = WeatherModel.Sys.Country;
                CidadePais = WeatherModel.Name + ", " + WeatherModel.Sys.Country;
                
                Icon = WeatherModel.Weather[0].Icon;
                string ImageUrl = $"https://openweathermap.org/img/wn/{Icon}@4x.png";
                WeatherImage = ImageSource.FromUri(new Uri(ImageUrl));

                Temp = Math.Round(WeatherModel.Main.Temp);
                Humidity = WeatherModel.Main.Humidity;
                Velocity = Math.Round(WeatherModel.Wind.Speed);
               
                Visibility = WeatherModel.Visibility/1000;
                FeelsLike = Math.Round(WeatherModel.Main.Feels_like);
                TempMin = Math.Round(WeatherModel.Main.Temp_min);
                TempMax = Math.Round(WeatherModel.Main.Temp_max);

                Description = WeatherModel.Weather[0].Description;

                DateTime utcNow = DateTime.UtcNow; /*Tempo em UTC*/
                TimeSpan timeZoneOffset = TimeSpan.FromSeconds(WeatherModel.Timezone);/*Diferença horária*/
                DateTimeOffset LocalTimezone = utcNow + timeZoneOffset;/*calculo horário local*/
                LocalTimezoneFormat = LocalTimezone.ToString("MMM d yyyy, HH:mm"); /*Formatação do Horário Local*/
            }
            

        }
        public async void GetCityWeather()
        {
            WeatherModel = await weatherAppService.GetWeatherAsync(CityName, Country);
            GetWeather(WeatherModel);
        }


        }

    
    
    
}
