using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class BreadcrumbViewModelBuilder : IViewModelBuilder<BreadcrumbViewModel>
{
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IPublishedUrlProvider _publishedUrlProvider;

    public BreadcrumbViewModelBuilder(IUmbracoContextAccessor umbracoContextAccessor, IPublishedUrlProvider publishedUrlProvider)
    {
        _umbracoContextAccessor = umbracoContextAccessor;
        _publishedUrlProvider = publishedUrlProvider;
    }

    public BreadcrumbViewModel Build()
    {
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext))
        {
            throw new InvalidOperationException("Could not retrieve Umbraco context");
        }

        var viewModel = new BreadcrumbViewModel();

        IPublishedContent? currentPage = umbracoContext.PublishedRequest?.PublishedContent;
        if (currentPage is null)
        {
            return viewModel;
        }

        viewModel.CurrentPageName = currentPage.Name;

        var links = new List<BreadcrumbViewModel.Link>();
        var ancestors = currentPage.Ancestors().Reverse().ToList();
        foreach (var ancestor in ancestors)
        {
            links.Add(
                new BreadcrumbViewModel.Link
                {
                    Text = ancestor.Name,
                    Url = ancestor.Url(_publishedUrlProvider),
                });
        }

        viewModel.Links = links;

        return viewModel;
    }
}
