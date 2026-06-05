using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.MenuItems;

public interface IMenuItemAppService : IApplicationService
{
    Task<MenuItemDto> GetAsync(Guid id);

    Task<PagedResultDto<MenuItemDto>> GetListAsync(GetMenuItemListDto input);

    Task<MenuItemDto> CreateAsync(CreateMenuItemDto input);

    Task UpdateAsync(Guid id, UpdateMenuItemDto input);

    Task DeleteAsync(Guid id);
}