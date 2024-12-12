using WeatherApp.ViewModels;

namespace WeatherApp.View;

public partial class WeatherAppView : ContentPage
{
	public WeatherAppView()
	{
		InitializeComponent();
		BindingContext = new WeatherAppViewModel();
        
		
    }
}