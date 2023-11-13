using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Attributes;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core.Controllers;

public class BlogArticlePageController : RenderController
{
    private readonly IViewModelBuilder<BlogArticlePage, BlogArticlePageViewModel> _viewModelBuilder;

    public BlogArticlePageController(
        ILogger<BlogArticlePageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IViewModelBuilder<BlogArticlePage, BlogArticlePageViewModel> viewModelBuilder)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    [ApplyPageMetaData]
    public override IActionResult Index()
    {
        var viewModel = _viewModelBuilder.Build(CurrentPage);
        return CurrentTemplate(viewModel);
    }
}
