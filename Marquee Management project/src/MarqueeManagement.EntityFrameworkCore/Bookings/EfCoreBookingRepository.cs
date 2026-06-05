using MarqueeManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MarqueeManagement.Bookings;

public class EfCoreBookingRepository : EfCoreRepository<MarqueeManagementDbContext, Booking, Guid>,
    IBookingRepository
{
    public EfCoreBookingRepository(
        IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    //public async Task<Booking> FindByEventTypeAsync(string eventType)
    //{
    //    var dbSet = await GetDbSetAsync();
    //    return await dbSet.FirstOrDefaultAsync(x => x.EventType == eventType);
    //}

    public async Task<List<Booking>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? eventType = null,
        BookingStatus? status = null)
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(Booking.EventDate);
        }

        var query = await GetQueryableAsync(filter, eventType, status);

        query = query.OrderBy(sorting)
                     .Skip(skipCount)
                     .Take(maxResultCount);

        return await query.ToListAsync();
    }

    public async Task<long> GetCountAsync(
        string? filter = null,
        string? eventType = null,
        BookingStatus? status = null)
    {
        var query = await GetQueryableAsync(filter, eventType, status);
        return await query.LongCountAsync();
    }

    private async Task<IQueryable<Booking>> GetQueryableAsync(
     string? filter = null,
     string? eventType = null,
     BookingStatus? status = null)
    {
        var dbSet = await GetDbSetAsync();

        var query = dbSet
           .Include(x => x.Marquee)       
           .Include(x => x.Customer)
           .Include(x => x.BookingMenuOptions)  
           .ThenInclude(o => o.MenuItem)   
            .AsQueryable();

        query = query
           .WhereIf(!filter.IsNullOrWhiteSpace(),
                x => x.EventType.Contains(filter!))
            .WhereIf(!eventType.IsNullOrWhiteSpace(),
                x => x.EventType.Contains(eventType!))
            .WhereIf(status != null,
                x => x.Status == status)
            .Where(x => !x.IsDeleted);

        return query;
    }

    public override async Task<IQueryable<Booking>> WithDetailsAsync()
    {
        var dbSet = await GetDbSetAsync();

        // EF Core: each .Include() = one SQL LEFT JOIN
        // .ThenInclude() = nested JOIN (join after a join)
        return dbSet
            .Include(b => b.Marquee)
            .Include(b => b.Customer)
            .Include(b => b.BookingMenuOptions)
                .ThenInclude(o => o.MenuItem);
    }
}