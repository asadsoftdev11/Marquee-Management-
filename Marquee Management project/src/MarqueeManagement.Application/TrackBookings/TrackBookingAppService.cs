using MarqueeManagement.BookingMenuOptions;
using MarqueeManagement.Bookings;
using MarqueeManagement.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.TrackBookings;

public class TrackBookingAppService : ApplicationService, ITrackBookingAppService
{
    private readonly IRepository<Customer, Guid> _customerRepo;
    private readonly IRepository<Booking, Guid> _bookingRepo;

    public TrackBookingAppService(IRepository<Customer, Guid> customerRepo,
                                     IRepository<Booking, Guid> bookingRepo)
    {
        _customerRepo = customerRepo;
        _bookingRepo = bookingRepo;
    }

    public async Task<TrackBookingResultDto> GetByNameOrPhoneAsync(
        string? name,
        string? phone)
    {
        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(phone))
            throw new UserFriendlyException("Please enter a name or phone number.");

        var customerQuery = await _customerRepo.GetQueryableAsync();

        var customer = customerQuery
            .WhereIf(!string.IsNullOrWhiteSpace(name),
                c => c.Name.ToLower().Contains(name.ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(phone),
                c => c.Phone.Contains(phone))
            .FirstOrDefault();


        if (customer == null) return null;

        var bookingQuery = await _bookingRepo.WithDetailsAsync();

        var bookings = bookingQuery
            .Where(b => b.CustomerId == customer.Id)
            .ToList();

        var result = new TrackBookingResultDto
        {
            CustomerName = customer.Name,
            CustomerPhone = customer.Phone,
            CustomerEmail = customer.Email,
            CustomerAddress = customer.Address,
        };

        foreach (var booking in bookings)
        {
            var bookingDto = new TrackBookingItemDto
            {
                BookingId = booking.Id,
                EventType = booking.EventType,
                EventDate = booking.EventDate,
                GuestCount = booking.GuestCount,
                TotalAmount = booking.TotalAmount,
                Status = (int)booking.Status,

                MarqueeName = booking.Marquee?.Name ?? "",
                MarqueeLocation = booking.Marquee?.Location ?? "",
                MarqueePricePerDay = booking.Marquee?.PricePerDay ?? 0,
                MarqueeCapacity = booking.Marquee?.Capacity ?? 1,
            };

            foreach (var opt in booking.BookingMenuOptions
                                ?? new List<BookingMenuOption>())
            {
                bookingDto.MenuOptions.Add(new TrackBookingMenuItemDto
                {
                    MenuItemName = opt.MenuItem?.Name ?? "Unknown",
                    Quantity = opt.Quantity,
                    PriceAtBookingTime = opt.PriceAtBookingTime,
                });
            }

            result.Bookings.Add(bookingDto);
        }

        return result;
    }
}