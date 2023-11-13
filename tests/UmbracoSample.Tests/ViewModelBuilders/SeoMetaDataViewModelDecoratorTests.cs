
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders;

public class SeoMetaDataViewModelDecoratorTests : ViewModelBuilderTestsBase
{
    [Test]
    public void Decorate_WithContentModel_UpdatesViewModel()
    {
        // Arrange
        var contentMock = new Mock<IPublishedContent>();
        var publishedValueFallbackMock = new Mock<IPublishedValueFallback>();

        const string PageTitle = "Test page title";
        const string MetaDescription = "Test meta description";
        const string MetaKeywords= "Test meta keywords";
        SetupPropertyValue(contentMock, GetPropertyAlias<HomePage>(nameof(HomePage.PageTitle)), PageTitle);
        SetupPropertyValue(contentMock, GetPropertyAlias<HomePage>(nameof(HomePage.MetaDescription)), MetaDescription);
        SetupPropertyValue(contentMock, GetPropertyAlias<HomePage>(nameof(HomePage.MetaKeywords)), MetaKeywords);

        var currentPage = new HomePage(contentMock.Object, publishedValueFallbackMock.Object);

        var viewModel = new HomePageViewModel();

        var sut = new SeoMetaDataViewModelDecorator();

        // Act
        sut.Decorate(viewModel, currentPage);

        // Assert
        viewModel.PageTitle.Should().Be(PageTitle);
        viewModel.MetaDescription.Should().Be(MetaDescription);
        viewModel.MetaKeywords.Should().Be(MetaKeywords);
    }
}