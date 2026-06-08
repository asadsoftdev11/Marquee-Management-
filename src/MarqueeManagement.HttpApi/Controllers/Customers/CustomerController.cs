using Asp.Versioning;
using MarqueeManagement.Customers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.Controllers.Customers;

[RemoteService(IsEnabled = true)]
[ControllerName("Customers")]
[Area("app")]
[Route("api/app/customers")]
public class CustomerController : AbpController, ICustomerAppService
{
    private readonly ICustomerAppService _customerAppService;

    public CustomerController(ICustomerAppService customerAppService)
    {
        _customerAppService = customerAppService;
    }

    [HttpGet("{id}")]
    public async Task<CustomerDto> GetAsync(Guid id)
    {
        return await _customerAppService.GetAsync(id);
    }

    [HttpGet]
    public async Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input)
    {
        return await _customerAppService.GetListAsync(input);
    }

    [HttpPost]
    public async Task<CustomerDto> CreateAsync(CreateCustomerDto input)
    {
        return await _customerAppService.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public async Task UpdateAsync(Guid id, UpdateCustomerDto input)
    {
        await _customerAppService.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _customerAppService.DeleteAsync(id);
    }
}