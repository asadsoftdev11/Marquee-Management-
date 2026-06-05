using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.MenuCategories;

public interface IMenuCategoryRepository : IRepository<MenuCategory, Guid>
{
    Task<MenuCategory> FindByNameAsync(string name);

    Task<List<MenuCategory>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? name = null
    );

    Task<long> GetCountAsync(
        string? filter = null,
        string? name = null
    );
}