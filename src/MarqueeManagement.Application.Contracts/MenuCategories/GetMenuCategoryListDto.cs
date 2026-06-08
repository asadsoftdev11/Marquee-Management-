using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuCategories;

public class GetMenuCategoryListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? Name { get; set; }
}
