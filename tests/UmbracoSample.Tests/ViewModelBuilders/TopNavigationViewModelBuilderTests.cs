using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Tests.ViewModelBuilders
{
    public class TopNavigationViewModelBuilderTests : ViewModelBuilderTestsBase
    {
        [Test]
        public void Build_ReturnsViewModel()
        {
            // Arrange
            var rootPageKey = Guid.NewGuid();
            var childPageKey1 = Guid.NewGuid();
            var childPageKey2 = Guid.NewGuid();
            var currentPageKey = Guid.NewGuid();

            const string RootPageName = "Root page";
            const string ChildPageName1 = "Child page 1";
            const string ChildPageName2 = "Child page 2";
            const string CurrentPageName = "Current page";

            var rootPageMock = CreateMockPublishedContent(1000, rootPageKey, RootPageName);
            var childPageMock1 = CreateMockPublishedContent(1001, childPageKey1, ChildPageName1, parent: rootPageMock.Object);
            var childPageMock2 = CreateMockPublishedContent(1002, childPageKey2, ChildPageName2, parent: rootPageMock.Object);
            rootPageMock
                .SetupGet(x => x.Children)
                .Returns(new List<IPublishedContent> { childPageMock1.Object, childPageMock2.Object });

            var currentPageMock = CreateMockPublishedContent(1003, currentPageKey, CurrentPageName, parent: childPageMock1.Object); // Under Child page 1.

            var publishedRequestMock = new Mock<IPublishedRequest>();
            publishedRequestMock
                .Setup(x => x.PublishedContent)
                .Returns(currentPageMock.Object);

            var publishedContentCacheMock = new Mock<IPublishedContentCache>();
            publishedContentCacheMock
                .Setup(x => x.GetAtRoot(It.IsAny<string>()))
                .Returns(new List<IPublishedContent> { rootPageMock.Object });

            var umbracoContextMock = new Mock<IUmbracoContext>();
            umbracoContextMock
                .Setup(x => x.PublishedRequest)
                .Returns(publishedRequestMock.Object);
            umbracoContextMock
                .Setup(x => x.Content)
                .Returns(publishedContentCacheMock.Object);
            
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
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == childPageKey1), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/child-1/");
            publishedUrlProviderMock
                .Setup(x => x.GetUrl(It.Is<IPublishedContent>(y => y.Key == childPageKey2), It.IsAny<UrlMode>(), It.IsAny<string?>(), It.IsAny<Uri?>()))
                .Returns("/child-2/");

            var sut = new TopNavigationViewModelBuilder(umbracoContextAccessorMock.Object, publishedUrlProviderMock.Object);

            // Act
            TopNavigationViewModel viewModel = sut.Build();

            // Assert
            viewModel.Links.Should().HaveCount(3);
            viewModel.Links[0].Text.Should().Be(RootPageName);
            viewModel.Links[0].Url.Should().Be("/");
            viewModel.Links[0].Active.Should().BeFalse();
            viewModel.Links[1].Text.Should().Be(ChildPageName1);
            viewModel.Links[1].Url.Should().Be("/child-1/");
            viewModel.Links[1].Active.Should().BeTrue();
            viewModel.Links[2].Text.Should().Be(ChildPageName2);
            viewModel.Links[2].Url.Should().Be("/child-2/");
            viewModel.Links[2].Active.Should().BeFalse();
        }
    }
}