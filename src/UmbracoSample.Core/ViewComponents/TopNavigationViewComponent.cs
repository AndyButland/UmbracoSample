using Microsoft.AspNetCore.Mvc;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core.ViewComponents;

public class TopNavigationViewComponent : ViewComponent
{
    private readonly IViewModelBuilder<TopNavigationViewModel> _viewModelBuilder;

    public TopNavigationViewComponent(IViewModelBuilder<TopNavigationViewModel> viewModelBuilder)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    public IViewComponentResult Invoke()
    {
        var viewModel = _viewModelBuilder.Build();
        return View(viewModel);
    }
}
