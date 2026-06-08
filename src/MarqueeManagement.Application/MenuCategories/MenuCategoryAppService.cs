using Microsoft.AspNetCore.Authorization;
using MarqueeManagement.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.MenuCategories;

[RemoteService(IsEnabled = false)]
[Authorize(MarqueeManagementPermissions.MenuCategories.Default)]
public class MenuCategoryAppService : MarqueeManagementAppService, IMenuCategoryAppService
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;
    private readonly MenuCategoryManager _menuCategoryManager;

    public MenuCategoryAppService(
        IMenuCategoryRepository menuCategoryRepository,
        MenuCategoryManager menuCategoryManager)
    {
        _menuCategoryRepository = menuCategoryRepository;
        _menuCategoryManager = menuCategoryManager;
    }

    public async Task<MenuCategoryDto> GetAsync(Guid id)
    {
        var entity = await _menuCategoryRepository.GetAsync(id);
        return ObjectMapper.Map<MenuCategory, MenuCategoryDto>(entity);
    }

    public async Task<PagedResultDto<MenuCategoryDto>> GetListAsync(GetMenuCategoryListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(MenuCategory.Name);
        }

        var list = await _menuCategoryRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter,
            input.Name
        );

        var totalCount = await _menuCategoryRepository.GetCountAsync(
            input.Filter,
            input.Name
        );

        var dtoList = ObjectMapper.Map<List<MenuCategory>, List<MenuCategoryDto>>(list);
        return new PagedResultDto<MenuCategoryDto>(totalCount, dtoList);
    }

    [Authorize(MarqueeManagementPermissions.MenuCategories.Create)]
    public async Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto input)
    {
        var menuItem = await _menuCategoryManager.CreateAsync(input.Name, input.Description);

        await _menuCategoryRepository.InsertAsync(menuItem);
        return ObjectMapper.Map<MenuCategory, MenuCategoryDto>(menuItem);
    }

    [Authorize(MarqueeManagementPermissions.MenuCategories.Edit)]
    public async Task UpdateAsync(Guid id, UpdateMenuCategoryDto input)
    {
        var menuItem = await _menuCategoryRepository.GetAsync(id);

        await _menuCategoryManager.UpdateAsync(menuItem, input.Name, input.Description);

        await _menuCategoryRepository.UpdateAsync(menuItem);
    }

    [Authorize(MarqueeManagementPermissions.MenuCategories.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _menuCategoryRepository.DeleteAsync(id);
    }
}