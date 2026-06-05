using Volo.Abp.Modularity;

namespace MarqueeManagement;

[DependsOn(
    typeof(MarqueeManagementApplicationModule),
    typeof(MarqueeManagementDomainTestModule)
)]
public class MarqueeManagementApplicationTestModule : AbpModule
{

}
