using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.BookingMenuOptions;

public interface IBookingMenuOptionRepository : IRepository<BookingMenuOption, Guid>
{
    Task<List<BookingMenuOption>> GetListAsync(
                    int skipCount,
                    int maxResultCount,
                    string sorting,
                    Guid? bookingId = null,
                    Guid? menuItemId = null,
                    string? filter = null
              );

    Task<long> GetCountAsync(
       Guid? bookingId = null,
       Guid? menuItemId = null,
       string? filter = null
    );
}
