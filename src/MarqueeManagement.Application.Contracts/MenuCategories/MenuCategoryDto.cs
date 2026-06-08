using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuCategories;

public class MenuCategoryDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}