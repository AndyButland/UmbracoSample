using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSample.Core.ViewModelBuilders;

public interface IViewModelDecorator<in TContentModel, in TViewModel>
    where TContentModel : IPublishedElement
{
    void Decorate(TViewModel viewModel, IPublishedElement? contentModel);
}

public interface IViewModelDecorator<in TContentModel, in TInputModel, in TViewModel>
    where TContentModel : IPublishedElement
{
    void Decorate(TViewModel viewModel, IPublishedElement? contentModel, TInputModel inputModel);
}
