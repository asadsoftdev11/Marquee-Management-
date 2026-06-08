using System.Threading.Tasks;

namespace MarqueeManagement.Data;

public interface IMarqueeManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
