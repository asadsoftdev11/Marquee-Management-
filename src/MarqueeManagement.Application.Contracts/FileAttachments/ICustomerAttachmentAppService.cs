using System;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.FileAttachments;

public interface ICustomerAttachmentAppService :
    ICrudAppService<
        CustomerAttachmentDto,
        Guid,
        Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto,
        CreateCustomerAttachmentDto>
{

}