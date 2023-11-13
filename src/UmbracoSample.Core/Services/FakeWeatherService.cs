namespace UmbracoSample.Core.Services
{
    internal class FakeWeatherService : IWeatherService
    {
        public Task<string> GetWeatherSummaryAsync()
        {
            Random rnd = new Random();
            int value = rnd.Next(1, 10);
            if (value == 1)
            {
                return Task.FromResult("Snowing");
            }
            else if (value > 1 && value < 5)
            {
                return Task.FromResult("Raining");
            }
            else
            {
                return Task.FromResult("Sunny");
            }
        }
    }
}
