using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace MarqueeManagement.MenuCategories;

public class MenuCategoryManager : DomainService
{
    private readonly IMenuCategoryRepository _menuCategoryRepository;

    public MenuCategoryManager(IMenuCategoryRepository menuCategoryRepository)
    {
        _menuCategoryRepository = menuCategoryRepository;
    }

    public async Task<MenuCategory> CreateAsync(
        string name,
        string? description
    )
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingCategory = await _menuCategoryRepository.FindByNameAsync(name);
        if (existingCategory != null)
        {
            throw new MenuCategoryAlreadyExistsException(name);
        }

        return new MenuCategory(
            GuidGenerator.Create(),
            name,
            description
        );
    }

    public async Task UpdateAsync(
        MenuCategory menuCategory,
        string name,
        string? description
    )
    {
        Check.NotNull(menuCategory, nameof(menuCategory));
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingCategory = await _menuCategoryRepository.FindByNameAsync(name);
        if (existingCategory != null && existingCategory.Id != menuCategory.Id)
        {
            throw new MenuCategoryAlreadyExistsException(name);
        }

        menuCategory.ChangeName(name);
        menuCategory.ChangeDescription(description);
    }
}