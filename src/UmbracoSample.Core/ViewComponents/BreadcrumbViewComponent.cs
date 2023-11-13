using Microsoft.AspNetCore.Mvc;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core.ViewComponents;

public class BreadcrumbViewComponent : ViewComponent
{
    private readonly IViewModelBuilder<BreadcrumbViewModel> _viewModelBuilder;

    public BreadcrumbViewComponent(IViewModelBuilder<BreadcrumbViewModel> viewModelBuilder)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    public IViewComponentResult Invoke()
    {
        var viewModel = _viewModelBuilder.Build();
        return View(viewModel);
    }
}
