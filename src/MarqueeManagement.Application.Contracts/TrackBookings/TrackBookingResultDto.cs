using System;
using System.Collections.Generic;
using System.Linq;

namespace MarqueeManagement.TrackBookings;

public class TrackBookingResultDto
{

    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }

    public List<TrackBookingItemDto> Bookings { get; set; }
        = new List<TrackBookingItemDto>();
}

public class TrackBookingItemDto
{
    public Guid BookingId { get; set; }
    public string EventType { get; set; }
    public DateTime EventDate { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalAmount { get; set; }
    public int Status { get; set; }
    public string MarqueeName { get; set; }
    public string MarqueeLocation { get; set; }
    public decimal MarqueePricePerDay { get; set; }
    public int MarqueeCapacity { get; set; }

    public decimal HallCharge =>  MarqueeCapacity > 0 ? (MarqueePricePerDay / MarqueeCapacity) * GuestCount : 0;
    public decimal FoodTotal =>  MenuOptions.Sum(o => o.LineTotal);
    public decimal GrandTotal => HallCharge + FoodTotal;
    public List<TrackBookingMenuItemDto> MenuOptions { get; set; }
        = new List<TrackBookingMenuItemDto>();
}

public class TrackBookingMenuItemDto
{
    public string MenuItemName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtBookingTime { get; set; }
    public decimal LineTotal => Quantity * PriceAtBookingTime;
}