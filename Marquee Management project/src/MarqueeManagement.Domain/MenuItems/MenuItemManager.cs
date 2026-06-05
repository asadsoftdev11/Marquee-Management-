using MarqueeManagement.MenuCategories;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.MenuItems;

public class MenuItemManager : DomainService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMenuCategoryRepository _menuCategoryRepository;
    public MenuItemManager(IMenuItemRepository menuItemRepository,
         IMenuCategoryRepository menuCategoryRepository)
    {
        _menuItemRepository = menuItemRepository;
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<MenuItem> CreateAsync(
        string name,
        string? description,
        int price,
        bool isAvailable,
        Guid menuCategoryId,
        string? imageUrl
        )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        var category = await _menuCategoryRepository.GetAsync(menuCategoryId);
        var existingMenuItem = await _menuItemRepository.FindByNameAsync(name);
        if (existingMenuItem != null)
        {
            throw new MenuItemAlreadyExistsException(name);
        }
        return new MenuItem(
            GuidGenerator.Create(),
            name,
            description,
            price,
            isAvailable,
            menuCategoryId,
            imageUrl
            );

    }
    public async Task UpdateAsync(
       MenuItem menuItem,
       string name,
       string? description,
       int price,
       bool isAvailable,
       Guid menuCategoryId,
       string? imageUrl
   )
    {
        Check.NotNull(menuItem, nameof(menuItem));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        var category = await _menuCategoryRepository.GetAsync(menuCategoryId);
        var existingMenuItem = await _menuItemRepository.FindByNameAsync(name);
        if (existingMenuItem != null && existingMenuItem.Id != menuItem.Id)
        {
            throw new MenuItemAlreadyExistsException(name);
        }

        menuItem.ChangeName(name)
               .ChangeDescription(description)
               .ChangePrice(price);
        menuItem.IsAvailable = isAvailable;
        menuItem.MenuCategoryId = menuCategoryId;
        menuItem.ImageUrl = imageUrl;
    }
}
