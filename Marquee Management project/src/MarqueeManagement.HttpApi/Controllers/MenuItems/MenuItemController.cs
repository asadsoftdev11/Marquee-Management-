using Asp.Versioning;
using MarqueeManagement.MenuItems;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Controllers.MenuItems;

[RemoteService(IsEnabled = true)]
[ControllerName("MenuItems")]
[Area("app")]
[Route("api/app/menu-items")]
public class MenuItemController : AbpController, IMenuItemAppService
{
    private readonly IMenuItemAppService _menuItemAppService;

    public MenuItemController(IMenuItemAppService menuItemAppService)
    {
        _menuItemAppService = menuItemAppService;
    }

    [HttpGet("{id}")]
    public async Task<MenuItemDto> GetAsync(Guid id)
    {
        return await _menuItemAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<MenuItemDto>> GetListAsync(GetMenuItemListDto input)
    {
        return await _menuItemAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto input)
    {
        return await _menuItemAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateMenuItemDto input)
    {
        await _menuItemAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _menuItemAppService.DeleteAsync(id);
    }
}