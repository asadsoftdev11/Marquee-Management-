using MarqueeManagement.BookingMenuOptions;
using MarqueeManagement.Customers;
using MarqueeManagement.Marquees;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.Bookings;

public class Booking : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public DateTime EventDate { get; set; }
    public string EventType { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalAmount { get; set; }
    public BookingStatus Status { get; set; }
    public Guid? TenantId { get; set; }
    public Guid MarqueeId { get; set; }
    public Marquee Marquee { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<BookingMenuOption> BookingMenuOptions { get; set; } = new List<BookingMenuOption>();

    private Booking()
    {
    }

    internal Booking(
        Guid id,
        Guid marqueeId,
        Guid customerId,
        DateTime eventDate,
        string eventType,
        int guestCount,
        decimal totalAmount,
        BookingStatus status
    ) : base(id)
    {
        MarqueeId = marqueeId;
        CustomerId = customerId;
        EventDate = eventDate;
         EventType = eventType;
         GuestCount = guestCount;
         TotalAmount = totalAmount;
         Status = status;
    }
}