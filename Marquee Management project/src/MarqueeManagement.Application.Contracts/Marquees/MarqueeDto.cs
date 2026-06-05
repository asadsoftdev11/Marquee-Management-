using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Marquees;

public class MarqueeDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerDay { get; set; }
}
