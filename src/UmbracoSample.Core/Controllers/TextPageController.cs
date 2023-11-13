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

public class TextPageController : RenderController
{
    private readonly IViewModelBuilder<TextPage, TextPageViewModel> _viewModelBuilder;

    public TextPageController(
        ILogger<TextPageController> logger,
        ICompositeViewEngine compositeViewEngine,
        IUmbracoContextAccessor umbracoContextAccessor,
        IViewModelBuilder<TextPage, TextPageViewModel> viewModelBuilder)
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
