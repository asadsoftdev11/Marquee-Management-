using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.BookingMenuOptions;

public class GetBookingMenuOptionListDto : PagedAndSortedResultRequestDto
{
    public Guid? BookingId { get; set; }
    public Guid? MenuItemId { get; set; }
    public string? Filter { get; set; }

}
