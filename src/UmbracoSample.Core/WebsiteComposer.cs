using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Configuration;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.Services;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core;

public class WebsiteComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        // Bind configuration.
        builder.Services.AddOptions<WebsiteSettings>()
           .BindConfiguration(nameof(WebsiteSettings));

        // Register services.
        builder.Services.AddSingleton<IWeatherService, FakeWeatherService>();

        // Register view model builders and decorators.
        builder.Services.AddSingleton<IViewModelDecorator<SEO, PageViewModelBase>, SeoMetaDataViewModelDecorator>();

        builder.Services.AddSingleton<IViewModelBuilder<TopNavigationViewModel>, TopNavigationViewModelBuilder>();
        builder.Services.AddSingleton<IViewModelBuilder<BreadcrumbViewModel>, BreadcrumbViewModelBuilder>();

        builder.Services.AddSingleton<IAsyncViewModelBuilder<HomePage, HomePageViewModel>, HomePageViewModelBuilder>();
        builder.Services.AddSingleton<IViewModelBuilder<BlogLandingPage, BlogLandingPageViewModelBuilderInput, BlogLandingPageViewModel>, BlogLandingPageViewModelBuilder>();
        builder.Services.AddSingleton<IViewModelBuilder<BlogArticlePage, BlogArticlePageViewModel>, BlogArticlePageViewModelBuilder>();
        builder.Services.AddSingleton<IViewModelBuilder<TextPage, TextPageViewModel>, TextPageViewModelBuilder>();
    }
}
