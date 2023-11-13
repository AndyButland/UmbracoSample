
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders
{
    public class BlogLandingPageViewModelBuilderTests : ViewModelBuilderTestsBase
    {
        [Test]
        public void Build_WithContentModel_ReturnsViewModel()
        {
            // Arrange
            var articlePageKey1 = Guid.NewGuid();
            var articlePageKey2 = Guid.NewGuid();
            var articlePageKey3 = Guid.NewGuid();

            const string Heading = "Test heading";
            const string Subheading = "Test subheading";

            var contentMock = new Mock<IPublishedContent>();
            SetupPropertyValue(contentMock, GetPropertyAlias<BlogLandingPage>(nameof(BlogLandingPage.Heading)), Heading);
            SetupPropertyValue(contentMock, GetPropertyAlias<BlogLandingPage>(nameof(BlogLandingPage.Subheading)), Subheading);

            var articlePageMock1 = CreateMockPublishedContent(1001, articlePageKey1, "Article One", DateTime.Now.AddDays(-5), BlogArticlePage.ModelTypeAlias, contentMock.Object);
            SetupPropertyValue(articlePageMock1, GetPropertyAlias<BlogArticlePage>(nameof(BlogArticlePage.Heading)), "Article one heading");
            var articlePageMock2 = CreateMockPublishedContent(1002, articlePageKey2, "Article Two", DateTime.Now.AddDays(-4), BlogArticlePage.ModelTypeAlias, contentMock.Object);
            SetupPropertyValue(articlePageMock2, GetPropertyAlias<BlogArticlePage>(nameof(BlogArticlePage.Heading)), "Article two heading"); 
            var articlePageMock3 = CreateMockPublishedContent(1003, articlePageKey3, "Article Three", DateTime.Now.AddDays(-3), BlogArticlePage.ModelTypeAlias, contentMock.Object);
            SetupPropertyValue(articlePageMock3, GetPropertyAlias<BlogArticlePage>(nameof(BlogArticlePage.Heading)), "Article three heading");

            var publishedValueFallbackMock = new Mock<IPublishedValueFallback>();

            var articlePage1 = new BlogArticlePage(articlePageMock1.Object, publishedValueFallbackMock.Object);
            var articlePage2 = new BlogArticlePage(articlePageMock2.Object, publishedValueFallbackMock.Object);
            var articlePage3 = new BlogArticlePage(articlePageMock3.Object, publishedValueFallbackMock.Object);

            contentMock
                .Setup(x => x.ChildrenForAllCultures)
                .Returns(new List<IPublishedContent> { articlePage1, articlePage2, articlePage3 });

            var currentPage = new BlogLandingPage(contentMock.Object, publishedValueFallbackMock.Object);

            var publishedUrlProviderMock = new Mock<IPublishedUrlProvider>();
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == articlePageKey1), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/blog/article-one/");
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == articlePageKey2), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/blog/article-two/");
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == articlePageKey3), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/blog/article-three/");

            var variationContextAccessorMock = new Mock<IVariationContextAccessor>();

            var inputModel = new BlogLandingPageViewModelBuilderInput
            {
                NumberOfRecentArticlesToDisplay = 2,
            };

            var sut = new BlogLandingPageViewModelBuilder(publishedUrlProviderMock.Object, variationContextAccessorMock.Object);

            // Act
            BlogLandingPageViewModel viewModel = sut.Build(currentPage, inputModel);

            // Assert
            viewModel.Heading.Should().Be(Heading);
            viewModel.Subheading.Should().Be(Subheading);

            viewModel.MostRecentArticles.Should().HaveCount(2);
            viewModel.MostRecentArticles[0].Heading.Should().Be("Article three heading");
            viewModel.MostRecentArticles[0].Url.Should().Be("/blog/article-three/");
            viewModel.MostRecentArticles[1].Heading.Should().Be("Article two heading");
            viewModel.MostRecentArticles[1].Url.Should().Be("/blog/article-two/");
        }
    }
}