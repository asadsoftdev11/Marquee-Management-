using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Marquees;

public class GetMarqueeListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
}
