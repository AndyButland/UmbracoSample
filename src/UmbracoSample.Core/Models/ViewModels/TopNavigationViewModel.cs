namespace UmbracoSample.Core.Models.ViewModels;

public class TopNavigationViewModel
{
    public IReadOnlyList<Link> Links { get; set; } = new List<Link>().AsReadOnly();

    public class Link
    {
        public string Text { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public bool Active { get; set; }
    }
}
