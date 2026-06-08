using MarqueeManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MarqueeManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MarqueeManagementEntityFrameworkCoreModule),
    typeof(MarqueeManagementApplicationContractsModule)
)]
public class MarqueeManagementDbMigratorModule : AbpModule
{
}
