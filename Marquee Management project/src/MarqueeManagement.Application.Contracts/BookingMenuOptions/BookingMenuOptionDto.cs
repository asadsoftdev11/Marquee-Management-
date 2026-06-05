using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.BookingMenuOptions;

public class BookingMenuOptionDto : EntityDto<Guid>
{
    public int Quantity { get; set; }
    public decimal PriceAtBookingTime { get; set; } 
    public Guid BookingId { get; set; }    
    public string BookingInfo { get; set; }
    public Guid MenuItemId { get; set; }
    public string MenuItemName { get; set; }

}
