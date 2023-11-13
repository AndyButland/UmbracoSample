using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.Services;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class HomePageViewModelBuilder : IAsyncViewModelBuilder<HomePage, HomePageViewModel>
{
    private readonly IWeatherService _weatherService;

    public HomePageViewModelBuilder(IWeatherService weatherService) => _weatherService = weatherService;

    public async Task<HomePageViewModel> BuildAsync(IPublishedContent? currentPage)
    {
        var contentModel = currentPage as HomePage
            ?? throw new ArgumentException("Provided published content is null or not of the expected content model type.", nameof(currentPage));

        var viewModel = new HomePageViewModel
        {
            Heading = contentModel.Heading ?? string.Empty,
        };

        viewModel.WeatherSummary = await _weatherService.GetWeatherSummaryAsync();

        return viewModel;
    }
}
