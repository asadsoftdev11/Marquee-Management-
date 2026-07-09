using System;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.FileAttachments;

public class FileAttachmentDto : EntityDto<Guid>
{
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public long Size { get; set; }
}