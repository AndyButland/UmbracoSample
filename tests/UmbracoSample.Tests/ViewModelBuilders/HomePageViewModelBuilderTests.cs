
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.Services;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders;

public class HomePageViewModelBuilderTests : ViewModelBuilderTestsBase
{
    [Test]
    public async Task Build_WithContentModel_ReturnsViewModel()
    {
        // Arrange
        var contentMock = new Mock<IPublishedContent>();
        var publishedValueFallbackMock = new Mock<IPublishedValueFallback>();

        const string Heading = "Test heading";            
        SetupPropertyValue(contentMock, GetPropertyAlias<HomePage>(nameof(HomePage.Heading)), Heading);

        var currentPage = new HomePage(contentMock.Object, publishedValueFallbackMock.Object);

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock
            .Setup(x => x.GetWeatherSummaryAsync())
            .ReturnsAsync("Sunny");

        var sut = new HomePageViewModelBuilder(weatherServiceMock.Object);

        // Act
        HomePageViewModel viewModel = await sut.BuildAsync(currentPage);

        // Assert
        viewModel.Heading.Should().Be(Heading);
    }
}