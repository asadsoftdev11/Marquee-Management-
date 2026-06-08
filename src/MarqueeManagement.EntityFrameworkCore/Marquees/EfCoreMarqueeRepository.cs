using MarqueeManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MarqueeManagement.Marquees;

public class EfCoreMarqueeRepository : EfCoreRepository<MarqueeManagementDbContext, Marquee, Guid>,
      IMarqueeRepository
{
    public EfCoreMarqueeRepository(
        IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<Marquee> FindByNameAsync(string name)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Marquee>> GetListAsync(
    int skipCount,
    int maxResultCount,
    string sorting,
    string? filter = null,
    string? name = null,
    string? location = null)
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(Marquee.Name);
        }

        var query = await GetQueryableAsync(filter, name, location);

        query = query.OrderBy(sorting)
                     .Skip(skipCount)
                     .Take(maxResultCount);

        return await query.ToListAsync();
    }

    public async Task<long> GetCountAsync(
      string? filter = null,
      string? name = null,
      string? location = null)
    {
        var query = await GetQueryableAsync(filter, name, location);
        return await query.LongCountAsync();
    }
    private async Task<IQueryable<Marquee>> GetQueryableAsync(
    string? filter = null,
    string? name = null,
    string? location = null)
    {
        var dbSet = await GetDbSetAsync();
        var query = dbSet.AsQueryable();

        query = query
            .WhereIf(!filter.IsNullOrWhiteSpace(),
                x => x.Name.Contains(filter!) || x.Location.Contains(filter!))
            .WhereIf(!name.IsNullOrWhiteSpace(),
                x => x.Name.Contains(name!))
            .WhereIf(!location.IsNullOrWhiteSpace(),
                x => x.Location.Contains(location!));

        return query;
    }

}
