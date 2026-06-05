using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuItems;

public class GetMenuItemListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? Name { get; set; }
    public bool? IsAvailable { get; set; }
}