using Volo.Abp.Modularity;

namespace MarqueeManagement;

[DependsOn(
    typeof(MarqueeManagementDomainModule),
    typeof(MarqueeManagementTestBaseModule)
)]
public class MarqueeManagementDomainTestModule : AbpModule
{

}
