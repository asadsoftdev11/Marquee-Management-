using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.Bookings;

public interface IBookingRepository : IRepository<Booking, Guid>
{
    //Task<Booking> FindByEventTypeAsync(string eventType);
    //Task<Booking> FindByMarqueeAndDateAsync(Guid marqueeId, DateTime eventDate);
    Task<List<Booking>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null,
        string? eventType = null,
      //Guid? marqueeId = null,
        BookingStatus? status = null
    );

    Task<long> GetCountAsync(
        string? filter = null,
        string? eventType = null,
       //Guid? marqueeId = null,
        BookingStatus? status = null
    );
}