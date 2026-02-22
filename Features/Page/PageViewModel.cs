using Microsoft.AspNetCore.Html;
using SpecKitUmbraco.Infrastructure.Rendering;

namespace SpecKitUmbraco.Features.Page;

public class PageViewModel
(
    IPublishedContent content,
    string culture
) : IViewModelBase
{
    public IPublishedContent Content { get; } = content;
    public string Culture { get; } = culture;

    public required string Title { get; init; }
    public required IHtmlContent Text { get; init; }
    public required IPublishedContent? Parent { get; init; }
    public required IPublishedContent[] Children { get; init; }
}
