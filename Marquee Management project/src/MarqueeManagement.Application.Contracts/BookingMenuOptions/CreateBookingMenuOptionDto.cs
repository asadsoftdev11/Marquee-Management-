using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.BookingMenuOptions;

public class CreateBookingMenuOptionDto
{
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal PriceAtBookingTime { get; set; }
    [Required]
    public Guid BookingId { get; set; }
    [Required]
    public Guid MenuItemId { get; set; }

}
