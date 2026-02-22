using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;

namespace SpecKitUmbraco.Infrastructure.Routing;

public class SurfaceControllerDependencies
(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider
)
{
    public IUmbracoContextAccessor UmbracoContextAccessor { get; } = umbracoContextAccessor;
    public IUmbracoDatabaseFactory DatabaseFactory { get; } = databaseFactory;
    public ServiceContext Services { get; } = services;
    public AppCaches AppCaches { get; } = appCaches;
    public IProfilingLogger ProfilingLogger { get; } = profilingLogger;
    public IPublishedUrlProvider PublishedUrlProvider { get; } = publishedUrlProvider;
}
