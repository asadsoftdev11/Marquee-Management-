using MarqueeManagement.MenuCategories;
using MarqueeManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuItems;
[RemoteService(IsEnabled = false)]
[Authorize(MarqueeManagementPermissions.MenuItems.Default)]
public class MenuItemAppService : MarqueeManagementAppService, IMenuItemAppService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly MenuItemManager _menuItemManager;

    public MenuItemAppService(
        IMenuItemRepository menuItemRepository,
        MenuItemManager menuItemManager)
    {
        _menuItemRepository = menuItemRepository;
        _menuItemManager = menuItemManager;
    }

    public async Task<MenuItemDto> GetAsync(Guid id)
    {
        var entity = await _menuItemRepository.GetAsync(id);
        return ObjectMapper.Map<MenuItem, MenuItemDto>(entity);
    }


    public async Task<PagedResultDto<MenuItemDto>> GetListAsync(GetMenuItemListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(MenuItem.Name);
        }

        var list = await _menuItemRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            input.Name,
            input.IsAvailable
        );

        var totalCount = await _menuItemRepository.GetCountAsync(
            input.Filter,
            input.Name,
            input.IsAvailable
        );

        var dtoList = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(list);

        return new PagedResultDto<MenuItemDto>(totalCount, dtoList);
    }

    [Authorize(MarqueeManagementPermissions.MenuItems.Create)]
    public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto input)
    {
        var menuItem = await _menuItemManager.CreateAsync(
            input.Name,
            input.Description,
            input.Price,
            input.IsAvailable,
            input.MenuCategoryId,
            input.ImageUrl
        );

        await _menuItemRepository.InsertAsync(menuItem);
        return ObjectMapper.Map<MenuItem, MenuItemDto>(menuItem);
    }

    [Authorize(MarqueeManagementPermissions.MenuItems.Edit)]
    public async Task UpdateAsync(Guid id, UpdateMenuItemDto input)
    {
        var menuItem = await _menuItemRepository.GetAsync(id);

        await _menuItemManager.UpdateAsync(
            menuItem,
            input.Name,
            input.Description,
            input.Price,
            input.IsAvailable,
            input.MenuCategoryId,
            input.ImageUrl
        );

        await _menuItemRepository.UpdateAsync(menuItem);
    }

    [Authorize(MarqueeManagementPermissions.MenuItems.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _menuItemRepository.DeleteAsync(id);
    }
}