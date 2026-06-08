using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MarqueeManagement.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class MarqueeManagementDbContextFactory : IDesignTimeDbContextFactory<MarqueeManagementDbContext>
{
    public MarqueeManagementDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        MarqueeManagementEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<MarqueeManagementDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));
        
        return new MarqueeManagementDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MarqueeManagement.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
