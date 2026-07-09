using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.FileAttachments;

public class CustomerAttachmentDto : EntityDto<Guid>
{
    public Guid CustomerId { get; set; }

    public Guid FileAttachmentId { get; set; }

    public string FileName { get; set; }
}