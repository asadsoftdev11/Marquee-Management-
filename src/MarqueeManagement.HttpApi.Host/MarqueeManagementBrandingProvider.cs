using Microsoft.Extensions.Localization;
using MarqueeManagement.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace MarqueeManagement;

[Dependency(ReplaceServices = true)]
public class MarqueeManagementBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MarqueeManagementResource> _localizer;

    public MarqueeManagementBrandingProvider(IStringLocalizer<MarqueeManagementResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
