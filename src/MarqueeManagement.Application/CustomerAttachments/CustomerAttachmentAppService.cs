using System;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.CustomerAttachments;

[RemoteService(IsEnabled = false)]
public class CustomerAttachmentAppService :
    CrudAppService<CustomerAttachment, CustomerAttachmentDto, Guid,
        PagedAndSortedResultRequestDto, CreateCustomerAttachmentDto>,
    ICustomerAttachmentAppService
{
    public CustomerAttachmentAppService(
        IRepository<CustomerAttachment, Guid> repository)
        : base(repository)
    {
    }
}
