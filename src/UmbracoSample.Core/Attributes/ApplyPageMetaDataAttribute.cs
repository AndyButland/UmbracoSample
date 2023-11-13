using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Sample.Core.Models.PublishedModels;
using UmbracoSample.Core.Models.ViewModels;
using UmbracoSample.Core.ViewModelBuilders;

namespace UmbracoSample.Core.Attributes;

public class ApplyPageMetaDataAttribute : TypeFilterAttribute
{
    public ApplyPageMetaDataAttribute() : base(typeof(ApplyPageMetaDataFilter))
    {
    }

    private class ApplyPageMetaDataFilter : IResultFilter
    {
        private readonly IViewModelDecorator<SEO, PageViewModelBase> _viewModelDecorator;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        public ApplyPageMetaDataFilter(IViewModelDecorator<SEO, PageViewModelBase> viewModelDecorator, IUmbracoContextAccessor umbracoContextAccessor)
        {
            _viewModelDecorator = viewModelDecorator;
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not ViewResult viewResult ||
                viewResult.Model is not PageViewModelBase viewModel ||
                !_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? umbracoContext))
            {
                return;
            }

            IPublishedContent? publishedContent = umbracoContext.PublishedRequest?.PublishedContent;
            if (publishedContent is null)
            {
                return;
            }

            _viewModelDecorator.Decorate(viewModel, publishedContent);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
