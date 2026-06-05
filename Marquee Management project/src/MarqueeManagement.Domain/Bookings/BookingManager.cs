using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.Bookings;

public class BookingManager : DomainService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingManager(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<Booking> CreateAsync(
        Guid marqueeId,
        Guid customerId,
        DateTime eventDate,
        string eventType,
        int guestCount,
        decimal totalAmount,
        BookingStatus status
    )
    {
        Check.NotNullOrWhiteSpace(eventType, nameof(eventType));
        Check.NotNull(status, nameof(status));

        // Only check if the hall is already booked for that date
        //var existingBooking = await _bookingRepository.FindByMarqueeAndDateAsync(marqueeId, eventDate);
        //if (existingBooking != null)
        //{
        //    throw new UserFriendlyException("This hall is already booked for the selected date.");
        //}


        return new Booking(
            GuidGenerator.Create(),
            marqueeId,
            customerId,
            eventDate,
            eventType,
            guestCount,
            totalAmount,
            status
        );
    }

    public async Task UpdateAsync(
        Booking booking,
        Guid marqueeId,
        Guid customerId,
        DateTime eventDate,
        string eventType,
        int guestCount,
        decimal totalAmount,
        BookingStatus status
    )
    {
        Check.NotNull(booking, nameof(booking));
        Check.NotNullOrWhiteSpace(eventType, nameof(eventType));
        Check.NotNull(status, nameof(status));

        // Only check if the hall is already booked for that date
        //var existingBooking = await _bookingRepository.FindByMarqueeAndDateAsync(marqueeId, eventDate);
        //if (existingBooking != null)
        //{
        //    throw new UserFriendlyException("This hall is already booked for the selected date.");
        //}

        booking.MarqueeId = marqueeId;
        booking.CustomerId = customerId;
        booking.EventDate = eventDate;
        booking.EventType = eventType;
        booking.GuestCount = guestCount;
        booking.TotalAmount = totalAmount;
        booking.Status = status;
    }
}