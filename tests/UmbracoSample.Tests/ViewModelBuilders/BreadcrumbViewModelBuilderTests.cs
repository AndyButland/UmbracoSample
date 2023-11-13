using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders
{
    public class BreadcrumbViewModelBuilderTests : ViewModelBuilderTestsBase
    {
        [Test]
        public void Build_ReturnsViewModel()
        {
            // Arrange
            var rootPageKey = Guid.NewGuid();
            var childPageKey = Guid.NewGuid();
            var currentPageKey = Guid.NewGuid();

            const string RootPageName = "Root page";
            const string ChildPageName = "Child page";
            const string CurrentPageName = "Grandchild page";

            var rootPageMock = CreateMockPublishedContent(1000, rootPageKey, RootPageName);
            var childPageMock = CreateMockPublishedContent(1001, childPageKey, ChildPageName, parent: rootPageMock.Object);
            var currentPageMock = CreateMockPublishedContent(1002, currentPageKey, CurrentPageName, parent: childPageMock.Object);

            var publishedRequestMock = new Mock<IPublishedRequest>();
            publishedRequestMock
                .Setup(x => x.PublishedContent)
                .Returns(currentPageMock.Object);
            var umbracoContextMock = new Mock<IUmbracoContext>();
            umbracoContextMock
                .Setup(x => x.PublishedRequest)
                .Returns(publishedRequestMock.Object);
            
            var umbracoContextAccessorMock = new Mock<IUmbracoContextAccessor>();
            var umbracoContext = umbracoContextMock.Object;
            umbracoContextAccessorMock
                .Setup(x => x.TryGetUmbracoContext(out umbracoContext))
                .Returns(true);

            var publishedUrlProviderMock = new Mock<IPublishedUrlProvider>();
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == rootPageKey), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/");
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == childPageKey), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/child/");

            var sut = new BreadcrumbViewModelBuilder(umbracoContextAccessorMock.Object, publishedUrlProviderMock.Object);

            // Act
            BreadcrumbViewModel viewModel = sut.Build();

            // Assert
            viewModel.Links.Should().HaveCount(2);
            viewModel.Links[0].Text.Should().Be(RootPageName);
            viewModel.Links[0].Url.Should().Be("/");
            viewModel.Links[1].Text.Should().Be(ChildPageName);
            viewModel.Links[1].Url.Should().Be("/child/");
            viewModel.CurrentPageName.Should().Be(CurrentPageName);
        }
    }
}