using MarqueeManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MarqueeManagementController : AbpControllerBase
{
    protected MarqueeManagementController()
    {
        LocalizationResource = typeof(MarqueeManagementResource);
    }
}
