using MarqueeManagement.BookingMenuOptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MarqueeManagement.EntityFrameworkCore;

public class EfCoreBookingMenuOptionRepository :
    EfCoreRepository<MarqueeManagementDbContext, BookingMenuOption, Guid>,
    IBookingMenuOptionRepository
{
    public EfCoreBookingMenuOptionRepository(
        IDbContextProvider<MarqueeManagementDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<List<BookingMenuOption>> GetListAsync(
                     int skipCount,
                    int maxResultCount,
                    string sorting,
                    Guid? bookingId = null,
                    Guid? menuItemId = null,
                    string? filter = null
        )
    {
        if (string.IsNullOrWhiteSpace(sorting))
        {
            sorting = nameof(BookingMenuOption.PriceAtBookingTime);
        }

        var dbSet = await GetDbSetAsync();
        return await dbSet
            .Include(x => x.Booking)
            .Include(x => x.MenuItem)
            .WhereIf(bookingId.HasValue, x => x.BookingId == bookingId)
            .WhereIf(menuItemId.HasValue, x => x.MenuItemId == menuItemId)
         .WhereIf(!filter.IsNullOrWhiteSpace(),
    x =>
        x.Quantity.ToString().Contains(filter) ||
        x.PriceAtBookingTime.ToString().Contains(filter) ||
        x.Booking.EventType.ToLower().Contains(filter.ToLower()) ||
        x.MenuItem.Name.ToLower().Contains(filter.ToLower())
)
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<long> GetCountAsync(
                    Guid? bookingId = null,
                    Guid? menuItemId = null,
                    string? filter = null
             )
    {
        var dbSet = await GetDbSetAsync();

        return await dbSet
            .Include(x => x.Booking)
            .Include(x => x.MenuItem)
            .WhereIf(bookingId.HasValue, x => x.BookingId == bookingId)
            .WhereIf(menuItemId.HasValue, x => x.MenuItemId == menuItemId)
            .WhereIf(!filter.IsNullOrWhiteSpace(),
    x =>
        x.Quantity.ToString().Contains(filter) ||
        x.PriceAtBookingTime.ToString().Contains(filter) ||
        x.Booking.EventType.ToLower().Contains(filter.ToLower()) ||
        x.MenuItem.Name.ToLower().Contains(filter.ToLower())
         )
            .LongCountAsync();
    }
}