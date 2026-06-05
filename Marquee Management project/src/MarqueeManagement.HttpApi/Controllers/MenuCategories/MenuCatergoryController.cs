using Asp.Versioning;
using MarqueeManagement.MenuCategories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Controllers.MenuCategories;

[RemoteService(IsEnabled = true)]
[ControllerName("MenuCategories")]
[Area("app")]
[Route("api/app/menu-categories")]
public class MenuCategoryController : AbpController, IMenuCategoryAppService
{
    private readonly IMenuCategoryAppService _menuCategoryAppService;

    public MenuCategoryController(IMenuCategoryAppService menuCategoryAppService)
    {
        _menuCategoryAppService = menuCategoryAppService;
    }

    [HttpGet("{id}")]
    public async Task<MenuCategoryDto> GetAsync(Guid id)
    {
        return await _menuCategoryAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<MenuCategoryDto>> GetListAsync(GetMenuCategoryListDto input)
    {
        return await _menuCategoryAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<MenuCategoryDto> CreateAsync(CreateMenuCategoryDto input)
    {
        return await _menuCategoryAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateMenuCategoryDto input)
    {
        await _menuCategoryAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _menuCategoryAppService.DeleteAsync(id);
    }
}