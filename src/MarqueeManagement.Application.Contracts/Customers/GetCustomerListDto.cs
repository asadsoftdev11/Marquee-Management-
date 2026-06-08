using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Customers;

public class GetCustomerListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}
