using Volo.Abp.Modularity;

namespace MarqueeManagement;

/* Inherit from this class for your domain layer tests. */
public abstract class MarqueeManagementDomainTestBase<TStartupModule> : MarqueeManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
