using System.ComponentModel.DataAnnotations;

namespace MarqueeManagement.Customers;

public class UpdateCustomerDto
{
    [Required]
    [StringLength(CustomerConsts.MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(CustomerConsts.MaxPhoneLength)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [StringLength(CustomerConsts.MaxEmailLength)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(CustomerConsts.MaxAddressLength)]
    public string Address { get; set; } = string.Empty;

}
