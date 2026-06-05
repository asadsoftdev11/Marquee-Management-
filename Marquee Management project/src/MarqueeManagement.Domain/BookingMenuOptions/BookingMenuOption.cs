using MarqueeManagement.Bookings;
using MarqueeManagement.MenuItems;
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.BookingMenuOptions;
public class BookingMenuOption : FullAuditedEntity<Guid>, IMultiTenant
{
    public int Quantity { get; set; }
    public decimal PriceAtBookingTime { get; set; }
    public Guid? TenantId { get; set; }
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; }
    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    private BookingMenuOption()
    {
    }
    internal BookingMenuOption(Guid id,
        Guid bookingId,
        Guid menuItemId,
        int quantity,
        decimal priceAtBookingTime
        ) : base(id)
    {
        BookingId = bookingId;
        MenuItemId = menuItemId;
        Quantity = quantity;
        PriceAtBookingTime = priceAtBookingTime;
    }
}