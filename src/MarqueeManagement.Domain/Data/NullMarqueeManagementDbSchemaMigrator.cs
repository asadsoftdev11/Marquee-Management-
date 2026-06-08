using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MarqueeManagement.Data;

/* This is used if database provider does't define
 * IMarqueeManagementDbSchemaMigrator implementation.
 */
public class NullMarqueeManagementDbSchemaMigrator : IMarqueeManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
