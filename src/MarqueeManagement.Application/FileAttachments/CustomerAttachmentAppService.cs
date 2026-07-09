using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MarqueeManagement.FileAttachments;

public class CustomerAttachmentAppService :
    CrudAppService<
        CustomerAttachment,
        CustomerAttachmentDto,
        Guid,
        Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto,
        CreateCustomerAttachmentDto>,
    ICustomerAttachmentAppService
{
    private readonly IRepository<CustomerAttachment, Guid> _repository;

    public CustomerAttachmentAppService(
        IRepository<CustomerAttachment, Guid> repository)
        : base(repository)
    {
        _repository = repository;
    }
}