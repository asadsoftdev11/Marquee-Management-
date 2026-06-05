using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.Customers;

public class CustomerDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

}
