using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSample.Core.ViewModelBuilders;

public interface IAsyncViewModelDecorator<in TContentModel, in TViewModel>
    where TContentModel : IPublishedElement
{
    Task DecorateAsync(TViewModel viewModel, IPublishedElement? contentModel);
}

public interface IAsyncViewModelDecorator<in TContentModel, in TInputModel, in TViewModel>
    where TContentModel : IPublishedElement
{
    Task DecorateAsync(TViewModel viewModel, IPublishedElement? contentModel, TInputModel inputModel);
}
