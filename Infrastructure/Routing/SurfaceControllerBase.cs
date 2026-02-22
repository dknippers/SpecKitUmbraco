using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Routing;
using Umbraco.Cms.Web.Website.Controllers;

namespace SpecKitUmbraco.Infrastructure.Routing;

public abstract class SurfaceControllerBase<T>
(
    SurfaceControllerDependencies deps
) : SurfaceController
(
    deps.UmbracoContextAccessor,
    deps.DatabaseFactory,
    deps.Services,
    deps.AppCaches,
    deps.ProfilingLogger,
    deps.PublishedUrlProvider
), IRenderController where T : PublishedContentWrapped
{
    protected new T CurrentPage =>
        base.CurrentPage is T typed
            ? typed
            : throw new InvalidCastException(
                $"CurrentPage is of type '{base.CurrentPage?.GetType().FullName ?? "null"}' " +
                $"but this controller requires '{typeof(T).FullName}'.");

    protected string Culture => HttpContext.Features.Get<UmbracoRouteValues>()?.PublishedRequest?.Culture ?? throw new NullReferenceException("Culture is null");
}
