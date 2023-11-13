using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSample.Core.ViewModelBuilders;

public interface IAsyncViewModelBuilder<TViewModel>
{
    Task<TViewModel> BuildAsync();
}

public interface IAsyncViewModelBuilder<in TContentModel, TViewModel>
    where TContentModel : IPublishedContent
{
    Task<TViewModel> BuildAsync(IPublishedContent? currentPage);
}

public interface IAsyncViewModelBuilder<in TContentModel, in TInputModel, TViewModel>
    where TContentModel : IPublishedContent
{
    Task<TViewModel> BuildAsync(IPublishedContent? currentPage, TInputModel inputModel);
}
