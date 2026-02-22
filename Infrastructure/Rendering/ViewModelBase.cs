namespace SpecKitUmbraco.Infrastructure.Rendering;

public interface IViewModelBase
{
    IPublishedContent Content { get; }
    string Culture { get; }
}
