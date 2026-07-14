using System;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.CustomerAttachments;

public interface ICustomerAttachmentAppService :
    ICrudAppService<
        CustomerAttachmentDto,
        Guid,
        Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto,
        CreateCustomerAttachmentDto>
{

}