namespace UmbracoSample.Core.Models.ViewModels;

public class HomePageViewModel : PageViewModelBase
{
    public string Heading { get; set; } = string.Empty;

    public bool HasWeatherSummary => !string.IsNullOrWhiteSpace(WeatherSummary);

    public string WeatherSummary { get; set; } = string.Empty;
}
