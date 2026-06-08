using MarqueeManagement.Bookings;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.Marquees;

public class Marquee : FullAuditedAggregateRoot<Guid> , IMultiTenant
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
    public Guid? TenantId { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public Marquee()
    {
    }
    internal Marquee(
        Guid id,
        string name,
        string location,
        string? description,
        int capacity,
        decimal pricePerDay
        ) : base(id)
    {
        SetName(name);
        SetLocation(location);
        SetDescription(description);
        SetCapacity(capacity);
        SetPricePerDay(pricePerDay);
    }
    internal Marquee ChangeName(string name)
    {
        SetName(name);
        return this;
    }
    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: MarqueeConsts.MaxNameLength
        );
    }
    internal Marquee ChangeLocation(string location)
    {
        SetLocation(location);
        return this;
    }
    internal Marquee ChangeCapacity(int capacity)
    {
        SetCapacity(capacity);
        return this;
    }

    internal Marquee ChangePricePerDay(decimal price)
    {
        SetPricePerDay(price);
        return this;
    }
    internal Marquee ChangeDescription(string description)
    {
        SetDescription(description);
        return this;
    }
    private void SetLocation(string location)
    {
        Location = Check.NotNullOrWhiteSpace(
            location,
            nameof(location),
            maxLength: MarqueeConsts.MaxLocationLength
        );
    }
    private void SetDescription(string? description)
    {
        if (!description.IsNullOrWhiteSpace())
        {
            Description = Check.Length(description,
                nameof(description),
                MarqueeConsts.MaxDescriptionLength
            );
        }
        else
        {
            Description = null;
        }
    }
    private void SetCapacity(int capacity)
    {
        Capacity = Check.Range(capacity, nameof(capacity),
            MarqueeConsts.MinCapacity,
            MarqueeConsts.MaxCapacity
        );
    }
    private void SetPricePerDay(decimal pricePerDay)
    {
        PricePerDay = Check.Range(pricePerDay, nameof(pricePerDay),
            MarqueeConsts.MinPricePerDay,
            MarqueeConsts.MaxPricePerDay
        );
    }
}

