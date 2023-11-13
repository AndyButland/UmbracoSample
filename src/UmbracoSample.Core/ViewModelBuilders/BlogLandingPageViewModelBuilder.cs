using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Extensions;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

public class BlogLandingPageViewModelBuilderInput
{
    public int NumberOfRecentArticlesToDisplay { get; set; }
}

internal class BlogLandingPageViewModelBuilder : IViewModelBuilder<BlogLandingPage, BlogLandingPageViewModelBuilderInput, BlogLandingPageViewModel>
{
    private readonly IPublishedUrlProvider _publishedUrlProvider;
    private readonly IVariationContextAccessor _variationContextAccessor;

    public BlogLandingPageViewModelBuilder(IPublishedUrlProvider publishedUrlProvider, IVariationContextAccessor variationContextAccessor)
    {
        _publishedUrlProvider = publishedUrlProvider;
        _variationContextAccessor = variationContextAccessor;
    }

    public BlogLandingPageViewModel Build(IPublishedContent? currentPage, BlogLandingPageViewModelBuilderInput inputModel)
    {
        var contentModel = currentPage as BlogLandingPage
            ?? throw new ArgumentException("Provided published content is null or not of the expected content model type.", nameof(currentPage));

        var viewModel = new BlogLandingPageViewModel
        {
            Heading = contentModel.Heading ?? string.Empty,
            Subheading = contentModel.Subheading ?? string.Empty,
        };

        var blogArticlePages = contentModel.Children<BlogArticlePage>(_variationContextAccessor)
            ?.OrderByDescending(x => x.CreateDate)
            .Take(inputModel.NumberOfRecentArticlesToDisplay)
            ?? Enumerable.Empty<BlogArticlePage>();
        var articles = new List<BlogLandingPageViewModel.Article>();
        foreach (BlogArticlePage article in blogArticlePages)
        {
            articles.Add(
                new BlogLandingPageViewModel.Article
                {
                    Heading = article.Heading ?? string.Empty,
                    Url = article.Url(_publishedUrlProvider),
                });
        }

        viewModel.MostRecentArticles = articles;

        return viewModel;
    }
}
