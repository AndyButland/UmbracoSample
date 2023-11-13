using System.Reflection;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Infrastructure.ModelsBuilder;

namespace UmbracoSample.Tests.ViewModelBuilders
{
    public abstract class ViewModelBuilderTestsBase
    {
        protected static Mock<IPublishedContent> CreateMockPublishedContent(
            int id,
            Guid key,
            string name,
            DateTime? created = null,
            string contentTypeAlias = "",
            IPublishedContent? parent = null)
        {
            var publishedContentMock = new Mock<IPublishedContent>();
            publishedContentMock
                .SetupGet(x => x.Id)
                .Returns(id);
            publishedContentMock
                .SetupGet(x => x.Key)
                .Returns(key);
            publishedContentMock
                .SetupGet(x => x.Name)
                .Returns(name);
            publishedContentMock
                .SetupGet(x => x.ContentType.ItemType)
                .Returns(PublishedItemType.Content);

            if (created.HasValue)
            {
                publishedContentMock
                    .SetupGet(x => x.CreateDate)
                    .Returns(created.Value);
            }

            if (!string.IsNullOrWhiteSpace(contentTypeAlias))
            {
                publishedContentMock
                    .SetupGet(x => x.ContentType.Alias)
                    .Returns(contentTypeAlias);
            }

            if (parent is not null)
            {
                publishedContentMock
                    .SetupGet(x => x.Parent)
                    .Returns(parent);
            }

            return publishedContentMock;
        }

        protected static string GetPropertyAlias<TContentModel>(string propertyName)
            where TContentModel : IPublishedContent
        {
            PropertyInfo? prop = typeof(TContentModel).GetProperties().SingleOrDefault(p => p.Name == propertyName)
                ?? throw new InvalidOperationException($"Could not find property: {propertyName}");
            object? attr = prop.GetCustomAttributes(false)
                .SingleOrDefault(a => a is ImplementPropertyTypeAttribute)
                ?? throw new InvalidOperationException($"Could not find attribute indicating property alias on property: {propertyName}");
            return ((ImplementPropertyTypeAttribute)attr).Alias;
        }

        protected static void SetupPropertyValue(Mock<IPublishedContent> contentMock, string propertyAlias, string propertyValue, string? culture = null)
        {
            var propertyMock = new Mock<IPublishedProperty>();
            propertyMock.Setup(x => x.Alias).Returns(propertyAlias);
            propertyMock.Setup(x => x.HasValue(culture, null)).Returns(true);
            propertyMock.Setup(x => x.GetValue(culture, null)).Returns(propertyValue);
            contentMock.Setup(x => x.GetProperty(propertyAlias)).Returns(propertyMock.Object);
        }
    }
}