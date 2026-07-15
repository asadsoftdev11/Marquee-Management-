using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace MarqueeManagement.CustomerAttachments;

public interface ICustomerAttachmentAppService :
    ICrudAppService<
        CustomerAttachmentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateCustomerAttachmentDto>
{

}