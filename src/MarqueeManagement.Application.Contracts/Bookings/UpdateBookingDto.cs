using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.Bookings;

public class UpdateBookingDto
{
    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public string EventType { get; set; }

    [Required]
    public int GuestCount { get; set; }

    [Required]
    public decimal TotalAmount { get; set; }

    [Required]
    public BookingStatus Status { get; set; }
    [Required]
    public Guid MarqueeId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
}
