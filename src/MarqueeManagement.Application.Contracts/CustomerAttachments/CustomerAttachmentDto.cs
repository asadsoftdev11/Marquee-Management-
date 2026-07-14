using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.CustomerAttachments;

public class CustomerAttachmentDto : EntityDto<Guid>
{
    public Guid CustomerId { get; set; }

    public Guid FileAttachmentId { get; set; }

    
} 