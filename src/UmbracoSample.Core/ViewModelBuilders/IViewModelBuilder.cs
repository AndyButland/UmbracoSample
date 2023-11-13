using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoSample.Core.ViewModelBuilders;

public interface IViewModelBuilder<out TViewModel>
{
    TViewModel Build();
}

public interface IViewModelBuilder<in TContentModel, out TViewModel>
    where TContentModel : IPublishedContent
{
    TViewModel Build(IPublishedContent? currentPage);
}

public interface IViewModelBuilder<in TContentModel, in TInputModel, out TViewModel>
    where TContentModel : IPublishedContent
{
    TViewModel Build(IPublishedContent? currentPage, TInputModel inputModel);
}
