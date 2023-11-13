
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders;

public class TextPageViewModelBuilderTests : ViewModelBuilderTestsBase
{
    [Test]
    public void Build_WithContentModel_ReturnsViewModel()
    {
        // Arrange
        var contentMock = new Mock<IPublishedContent>();
        var publishedValueFallbackMock = new Mock<IPublishedValueFallback>();

        const string Heading = "Test heading";            
        SetupPropertyValue(contentMock, GetPropertyAlias<TextPage>(nameof(TextPage.Heading)), Heading);

        var currentPage = new TextPage(contentMock.Object, publishedValueFallbackMock.Object);

        var sut = new TextPageViewModelBuilder();

        // Act
        TextPageViewModel viewModel = sut.Build(currentPage);

        // Assert
        viewModel.Heading.Should().Be(Heading);
    }
}