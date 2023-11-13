
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders
{
    public class BlogArticlePageViewModelBuilderTests : ViewModelBuilderTestsBase
    {
        [Test]
        public void Build_WithContentModel_ReturnsViewModel()
        {
            // Arrange
            var contentMock = new Mock<IPublishedContent>();
            var publishedValueFallbackMock = new Mock<IPublishedValueFallback>();

            const string Heading = "Test heading";            
            SetupPropertyValue(contentMock, GetPropertyAlias<BlogArticlePage>(nameof(BlogArticlePage.Heading)), Heading);

            var currentPage = new BlogArticlePage(contentMock.Object, publishedValueFallbackMock.Object);

            var sut = new BlogArticlePageViewModelBuilder();

            // Act
            BlogArticlePageViewModel viewModel = sut.Build(currentPage);

            // Assert
            viewModel.Heading.Should().Be(Heading);
        }
    }
}