using System;
using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.MenuItems;

public class CreateMenuItemDto
{
    [Required]
    [StringLength(MenuItemConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [StringLength(MenuItemConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    [Range(MenuItemConsts.MinPriceValue, MenuItemConsts.MaxPriceValue)]
    public int Price { get; set; }

    public bool IsAvailable { get; set; }

    [Required]
    public Guid MenuCategoryId { get; set; }
    public string? ImageUrl { get; set; }
}