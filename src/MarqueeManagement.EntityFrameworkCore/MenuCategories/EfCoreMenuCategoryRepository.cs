using MarqueeManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MarqueeManagement.MenuCategories;

public class EfCoreMenuCategoryRepository :
    EfCoreRepository<MarqueeManagementDbContext, MenuCategory, Guid>,
    IMenuCategoryRepository
{
    public EfCoreMenuCategoryRepository(
        IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<MenuCategory> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<MenuCategory>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? name = null)
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(MenuCategory.Name);
        }

        var query = await GetQueryableAsync(filter, name);

        query = query.OrderBy(sorting)
                     .Skip(skipCount)
                     .Take(maxResultCount);

        return await query.ToListAsync();
    }

    public async Task<long> GetCountAsync(
        string? filter = null,
        string? name = null)
    {
        var query = await GetQueryableAsync(filter, name);
        return await query.LongCountAsync();
    }

    private async Task<IQueryable<MenuCategory>> GetQueryableAsync(
        string? filter = null,
        string? name = null)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.AsQueryable();

        query = query
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter))
            .WhereIf(!name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(name));

        return query;
    }
}