namespace UmbracoSample.Core.Models.ViewModels;

public class BreadcrumbViewModel
{
    public IReadOnlyList<Link> Links { get; set; } = new List<Link>().AsReadOnly();

    public string CurrentPageName { get; set; } = string.Empty;

    public class Link
    {
        public string Text { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
