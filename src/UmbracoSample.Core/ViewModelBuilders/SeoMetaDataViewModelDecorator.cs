using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class SeoMetaDataViewModelDecorator : IViewModelDecorator<ISEO, PageViewModelBase>
{
    public void Decorate(PageViewModelBase viewModel, IPublishedElement? currentPage)
    {
        var contentModel = currentPage as ISEO
            ?? throw new ArgumentException($"Provided published content is null or not of the expected content model type ({typeof(ISEO)}).", nameof(currentPage));

        viewModel.PageTitle = contentModel.PageTitle ?? string.Empty;
        viewModel.MetaDescription = contentModel.MetaDescription ?? string.Empty;
        viewModel.MetaKeywords = contentModel.MetaKeywords ?? string.Empty;
    }
}
