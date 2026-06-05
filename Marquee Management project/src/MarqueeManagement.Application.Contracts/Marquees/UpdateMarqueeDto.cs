using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.Marquees;

public class UpdateMarqueeDto
{
    [Required]
    [StringLength(MarqueeConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(MarqueeConsts.MaxLocationLength)]
    public string Location { get; set; } = string.Empty;

    [StringLength(MarqueeConsts.MaxDescriptionLength)]
    public string? Description { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public decimal PricePerDay { get; set; }

}
