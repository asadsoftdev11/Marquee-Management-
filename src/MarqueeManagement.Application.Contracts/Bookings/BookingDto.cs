using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Bookings;

public class BookingDto : EntityDto<Guid>
{
    public DateTime EventDate { get; set; }
    public string EventType { get; set; }
    public int GuestCount { get; set; }
    public decimal TotalAmount { get; set; }
    public BookingStatus Status { get; set; }
    public Guid MarqueeId { get; set; }
    public string MarqueeName { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
}
