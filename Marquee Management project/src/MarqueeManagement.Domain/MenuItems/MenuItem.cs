using MarqueeManagement.BookingMenuOptions;
using MarqueeManagement.MenuCategories;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.MenuItems;

public class MenuItem : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
    public Guid? TenantId { get; set; }
    public Guid MenuCategoryId { get; set; } 
    public MenuCategory MenuCategory { get; set; }
    public ICollection<BookingMenuOption> BookingMenuOptions { get; set; } = new List<BookingMenuOption>();
    public string? ImageUrl { get; set; }
    public MenuItem()
    {
    }

    internal MenuItem(Guid id,
        string name,
        string? description,
        int price,
        bool isAvailable,
        Guid menuCategoryId,
         string? imageUrl
        ) : base(id)
    {
        SetName(name);
        SetDescription(description);
        SetPrice(price);
        IsAvailable = isAvailable;
        MenuCategoryId = menuCategoryId;
        ImageUrl = imageUrl;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: MenuItemConsts.MaxNameLength
        );
    }

    internal MenuItem ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    internal MenuItem ChangeDescription(string? description)
    {
        SetDescription(description);
        return this;
    }

    private void SetDescription(string? description)
    {
        if (!description.IsNullOrWhiteSpace())
        {
            Description = Check.Length(description,
                nameof(description),
                MenuItemConsts.MaxDescriptionLength
            );
        }
        else
        {
            Description = null;
        }
    }
    internal MenuItem ChangePrice(int price)
    {
        SetPrice(price);
        return this;
    }
    private void SetPrice(int price)
    {
        Price = Check.Range(
            price,
            nameof(price),
            minimumValue: MenuItemConsts.MinPriceValue,
            maximumValue: MenuItemConsts.MaxPriceValue
        );
    }
}
