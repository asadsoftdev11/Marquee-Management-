using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MarqueeManagement.Data;
using Volo.Abp.DependencyInjection;

namespace MarqueeManagement.EntityFrameworkCore;

public class EntityFrameworkCoreMarqueeManagementDbSchemaMigrator
    : IMarqueeManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMarqueeManagementDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MarqueeManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MarqueeManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
