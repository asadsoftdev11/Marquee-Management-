using MarqueeManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace MarqueeManagement.MenuItems;


public class EfCoreMenuItemRepository : EfCoreRepository<MarqueeManagementDbContext,
    MenuItem, Guid>,
      IMenuItemRepository
{
    public EfCoreMenuItemRepository(
       IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
       : base(dbContextProvider)
    {
    }
    public async Task<MenuItem?> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<MenuItem>> GetListAsync(
         int skipCount,
         int maxResultCount,
         string sorting,
         string? filter = null,
         string? name = null,
         bool? isAvailable = null
     )
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(MenuItem.Name);
        }

        var query = await GetQueryableAsync(filter, name, isAvailable);

        query = query.OrderBy(sorting)
                     .Skip(skipCount)
                     .Take(maxResultCount);

        return await query.ToListAsync();
    }


    public async Task<long> GetCountAsync(
      string? filter = null,
      string? name = null,
      bool? isAvailable = null
       )
    {
        var query = await GetQueryableAsync(filter, name, isAvailable);
        return await query.LongCountAsync();
    }
    private async Task<IQueryable<MenuItem>> GetQueryableAsync(
       string? filter = null,
       string? name = null,
       bool? isAvailable = null)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet
           .Include(x => x.MenuCategory)
           .AsQueryable();

        query = query
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter!))
            .WhereIf(!name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(name!))
            .WhereIf(isAvailable.HasValue,
                x => x.IsAvailable == isAvailable);

        return query;
    }
}