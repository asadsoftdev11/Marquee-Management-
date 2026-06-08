using Volo.Abp.Modularity;

namespace MarqueeManagement;

public abstract class MarqueeManagementApplicationTestBase<TStartupModule> : MarqueeManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
