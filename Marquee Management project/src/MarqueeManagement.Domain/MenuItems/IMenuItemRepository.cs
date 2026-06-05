using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.MenuItems;

public interface IMenuItemRepository : IRepository<MenuItem, Guid>
{
    Task<MenuItem?> FindByNameAsync(string name);
    Task<List<MenuItem>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? name = null,
        bool? isAvailable = null
    );
    Task<long> GetCountAsync(
      string? filter = null,
      string? name = null,
      bool? isAvailable = null
      );

}
