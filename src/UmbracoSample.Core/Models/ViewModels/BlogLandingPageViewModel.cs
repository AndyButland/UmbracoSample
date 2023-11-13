namespace UmbracoSample.Core.Models.ViewModels;

public class BlogLandingPageViewModel : PageViewModelBase
{
    public string Heading { get; set; } = string.Empty;

    public string Subheading { get; set; } = string.Empty;

    public IReadOnlyList<Article> MostRecentArticles { get; set; } = new List<Article>().AsReadOnly();

    public class Article
    {
        public string Heading { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
