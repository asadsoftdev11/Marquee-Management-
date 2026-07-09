using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.FileAttachments;

public class CreateCustomerAttachmentDto
{
    public Guid CustomerId { get; set; }

    public Guid FileAttachmentId { get; set; }
}