using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using SpecKitUmbraco.Infrastructure.Routing;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services.Navigation;

namespace SpecKitUmbraco.Features.Page;

public class PageController
(
    SurfaceControllerDependencies deps,
    IDocumentNavigationQueryService docNavQueryService
) : SurfaceControllerBase<M.Page>(deps)
{
    public IActionResult Index()
    {
        var parent = GetParent();
        var children = GetChildren();

        var vm = new PageViewModel(CurrentPage, Culture)
        {
            Title = CurrentPage.Title ?? CurrentPage.Name,
            Text = new HtmlString(CurrentPage.Text?.ToHtmlString() ?? ""),
            Parent = parent,
            Children = children
        };

        return View("/Features/Page/Page.cshtml", vm);
    }

    private IPublishedContent? GetParent()
    {
        if (UmbracoContext.Content is not IPublishedContentCache contentCache ||
            !docNavQueryService.TryGetParentKey(CurrentPage.Key, out var parentKey) ||
            parentKey is null ||
            contentCache.GetById(parentKey.Value) is not IPublishedContent parent)
        {
            return null;
        }

        return parent;
    }

    private IPublishedContent[] GetChildren()
    {
        if (UmbracoContext.Content is not IPublishedContentCache contentCache ||
            !docNavQueryService.TryGetChildrenKeys(CurrentPage.Key, out var keys) ||
            keys?.ToArray() is not Guid[] childKeys ||
            childKeys.Length == 0)
        {
            return [];
        }

        return [.. childKeys
            .Select(contentCache.GetById)
            .OfType<IPublishedContent>()
            .OrderBy(ipc => ipc.SortOrder)];
    }
}
