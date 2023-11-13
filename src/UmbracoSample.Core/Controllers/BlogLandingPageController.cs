using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Attributes;
using UmbracoSample.Core.Configuration;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core.Controllers;

public class BlogLandingPageController : RenderController
{
    private readonly IViewModelBuilder<BlogLandingPage, BlogLandingPageViewModelBuilderInput, BlogLandingPageViewModel> _viewModelBuilder;
    private readonly IOptionsMonitor<WebsiteSettings> _websiteSettings;

    public BlogLandingPageController(
        ILogger<BlogLandingPageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IViewModelBuilder<BlogLandingPage, BlogLandingPageViewModelBuilderInput, BlogLandingPageViewModel> viewModelBuilder,
        IOptionsMonitor<WebsiteSettings> websiteSettings)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _viewModelBuilder = viewModelBuilder;
        _websiteSettings = websiteSettings;
    }

    [ApplyPageMetaData]
    public override IActionResult Index()
    {
        var inputModel = new BlogLandingPageViewModelBuilderInput
        {
            NumberOfRecentArticlesToDisplay = _websiteSettings.CurrentValue.NumberOfRecentArticlesToDisplay,
        };
        var viewModel = _viewModelBuilder.Build(CurrentPage, inputModel);
        return CurrentTemplate(viewModel);
    }
}
