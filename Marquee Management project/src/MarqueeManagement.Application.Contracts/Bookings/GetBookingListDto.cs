using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Bookings;

public class GetBookingListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? EventType { get; set; }
    public BookingStatus? Status { get; set; }

}
