namespace UmbracoSample.Core.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherSummaryAsync();
    }
}
