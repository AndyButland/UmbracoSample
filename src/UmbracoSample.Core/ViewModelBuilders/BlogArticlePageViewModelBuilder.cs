using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class BlogArticlePageViewModelBuilder : IViewModelBuilder<BlogArticlePage, BlogArticlePageViewModel>
{
    public BlogArticlePageViewModel Build(IPublishedContent? currentPage)
    {
        var contentModel = currentPage as BlogArticlePage
            ?? throw new ArgumentException("Provided published content is null or not of the expected content model type.", nameof(currentPage));

        return new BlogArticlePageViewModel
        {
            Heading = contentModel.Heading ?? string.Empty,
        };
    }
}
