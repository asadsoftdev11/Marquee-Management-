using System;
using MarqueeManagement.Customers;
using MarqueeManagement.FileAttachments;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.CustomerAttachments;

public class CustomerAttachment :
    FullAuditedAggregateRoot<Guid>,
    IMultiTenant
{
    public Guid? TenantId { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid FileAttachmentId { get; private set; }

    public Customer Customer { get; set; }

    public FileAttachment FileAttachment { get; set; }

    protected CustomerAttachment()
    {
    }

    public CustomerAttachment(
        Guid id,
        Guid? tenantId,
        Guid customerId,
        Guid fileAttachmentId)
        : base(id)
    {
        TenantId = tenantId;
        CustomerId = customerId;
        FileAttachmentId = fileAttachmentId;
    }
}