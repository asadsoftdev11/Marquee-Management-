using MarqueeManagement.Localization;
using Volo.Abp.Application.Services;

namespace MarqueeManagement;

/* Inherit your application services from this class.
 */
public abstract class MarqueeManagementAppService : ApplicationService
{
    protected MarqueeManagementAppService()
    {
        LocalizationResource = typeof(MarqueeManagementResource);
    }
}
