using MarqueeManagement.MenuItems;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.MenuCategories;

public class MenuCategory : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public Guid? TenantId { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    private MenuCategory()
    {
    }

    internal MenuCategory(
        Guid id,
        string name,
        string? description
    ) : base(id)
    {
        SetName(name);
        SetDescription(description);
    }

    internal MenuCategory ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    internal MenuCategory ChangeDescription(string? description)
    {
        SetDescription(description);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: MenuCategoryConsts.MaxNameLength
        );
    }

    private void SetDescription(string? description)
    {
        if (!description.IsNullOrWhiteSpace())
        {
            Description = Check.Length(
                description,
                nameof(description),
                MenuCategoryConsts.MaxDescriptionLength
            );
        }
        else
        {
            Description = null;
        }
    }
}