using SpecKitUmbraco.Features.AutoLogin;
using SpecKitUmbraco.Infrastructure.Routing;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .AddAutoLogin("user@example.com")
    .Build();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

    // Enable runtime compilation of .cshtml files -- no longer enabled by default in Umbraco 17+
    builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
}

builder.Services.AddScoped<SurfaceControllerDependencies>();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
