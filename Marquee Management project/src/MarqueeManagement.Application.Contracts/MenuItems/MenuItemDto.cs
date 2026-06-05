using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuItems;

public class MenuItemDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
    public Guid MenuCategoryId { get; set; }
    public string MenuCategoryName { get; set; }
    public string? ImageUrl { get; set; }
}