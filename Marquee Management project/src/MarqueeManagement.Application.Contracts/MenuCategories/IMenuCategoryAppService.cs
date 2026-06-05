using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.MenuCategories;

public interface IMenuCategoryAppService : IApplicationService
{
    Task<MenuCategoryDto> GetAsync(Guid id);
    Task<PagedResultDto<MenuCategoryDto>> GetListAsync(GetMenuCategoryListDto input);
    Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto input);
    Task UpdateAsync(Guid id, UpdateMenuCategoryDto input);
    Task DeleteAsync(Guid id);
}
