using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;

namespace UmbracoSample.Core.ViewModelBuilders;

internal class TextPageViewModelBuilder : IViewModelBuilder<TextPage, TextPageViewModel>
{
    public TextPageViewModel Build(IPublishedContent? currentPage)
    {
        var contentModel = currentPage as TextPage
            ?? throw new ArgumentException("Provided published content is null or not of the expected content model type.", nameof(currentPage));

        return new TextPageViewModel
        {
            Heading = contentModel.Heading ?? string.Empty,
        };
    }
}
