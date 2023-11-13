namespace UmbracoSample.Core.Models.ViewModels;

public abstract class PageViewModelBase
{
    public string PageTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;

    public string MetaKeywords { get; set; } = string.Empty;
}
