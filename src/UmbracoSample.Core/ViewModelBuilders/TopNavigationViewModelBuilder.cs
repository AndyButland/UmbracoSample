using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class TopNavigationViewModelBuilder : IViewModelBuilder<TopNavigationViewModel>
{
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IPublishedUrlProvider _publishedUrlProvider;

    public TopNavigationViewModelBuilder(IUmbracoContextAccessor umbracoContextAccessor, IPublishedUrlProvider publishedUrlProvider)
    {
        _umbracoContextAccessor = umbracoContextAccessor;
        _publishedUrlProvider = publishedUrlProvider;
    }

    public TopNavigationViewModel Build()
    {
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext))
        {
            throw new InvalidOperationException("Could not retrieve Umbraco context");
        }

        var viewModel = new TopNavigationViewModel();
        IPublishedContent? homePage = umbracoContext.Content?.GetAtRoot().FirstOrDefault();
        if (homePage is null)
        {
            return viewModel;
        }

        int currentSectionRootId = GetCurrentSectionRootId(umbracoContext, homePage);

        var links = new List<TopNavigationViewModel.Link>();
        AddItem(links, homePage, currentSectionRootId);

        foreach (var childPage in homePage.Children)
        {
            AddItem(links, childPage, currentSectionRootId);
        }

        viewModel.Links = links;

        return viewModel;
    }

    private static int GetCurrentSectionRootId(IUmbracoContext umbracoContext, IPublishedContent homePage)
    {
        var currentSectionRootId = 0;
        var currentPage = umbracoContext.PublishedRequest?.PublishedContent;
        if (currentPage is not null)
        {
            if (currentPage.Id == homePage.Id)
            {
                currentSectionRootId = currentPage.Id;
            }
            else
            {
                IEnumerable<IPublishedContent> currentPageAncestors = currentPage.AncestorsOrSelf();
                var sectionPage = currentPageAncestors.Skip(1).FirstOrDefault();
                if (sectionPage is not null)
                {
                    currentSectionRootId = sectionPage.Id;
                }
            }
        }

        return currentSectionRootId;
    }

    private void AddItem(List<TopNavigationViewModel.Link> links, IPublishedContent content, int currentItemId) =>
        links.Add(
            new TopNavigationViewModel.Link
            { 
                Text = content.Name,
                Url = content.Url(_publishedUrlProvider),
                Active = content.Id == currentItemId
            });
}
