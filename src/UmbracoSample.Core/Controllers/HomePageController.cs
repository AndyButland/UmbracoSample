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

public class HomePageController : RenderController
{
    private readonly IAsyncViewModelBuilder<HomePage, HomePageViewModel> _viewModelBuilder;

    public HomePageController(
        ILogger<HomePageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IAsyncViewModelBuilder<HomePage, HomePageViewModel> viewModelBuilder)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    [NonAction]
    public sealed override IActionResult Index() => throw new NotImplementedException();

    [ApplyPageMetaData]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = await _viewModelBuilder.BuildAsync(CurrentPage);
        return CurrentTemplate(viewModel);
    }
}
