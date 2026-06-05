using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.MenuCategories;

public class UpdateMenuCategoryDto
{
    [Required]
    [MaxLength(MenuCategoryConsts.MaxNameLength)]
    public string Name { get; set; }

    [MaxLength(MenuCategoryConsts.MaxDescriptionLength)]
    public string? Description { get; set; }
}
