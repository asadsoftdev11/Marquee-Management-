using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.CustomerAttachments;

[ApiVersion("1.0")] 
[ControllerName("CustomerAttachments")]
[Area("app")]
[Route("api/app/customer-attachment")]
public class CustomerAttachmentController : AbpController
{
    private readonly ICustomerAttachmentAppService _customerAttachmentAppService;

    public CustomerAttachmentController(
        ICustomerAttachmentAppService customerAttachmentAppService)
    {
        _customerAttachmentAppService = customerAttachmentAppService;
    }

    [HttpPost]
    public async Task<CustomerAttachmentDto> CreateAsync(
        CreateCustomerAttachmentDto input)
    {
        return await _customerAttachmentAppService.CreateAsync(input);
    }

    [HttpGet]
    public async Task<PagedResultDto<CustomerAttachmentDto>> GetListAsync(
        [FromQuery] PagedAndSortedResultRequestDto input) 
    {
        return await _customerAttachmentAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public async Task<CustomerAttachmentDto> GetAsync(Guid id)
    {
        return await _customerAttachmentAppService.GetAsync(id);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _customerAttachmentAppService.DeleteAsync(id);
    }
}
