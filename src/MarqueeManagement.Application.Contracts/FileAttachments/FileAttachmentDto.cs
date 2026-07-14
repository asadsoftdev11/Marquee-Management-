using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace MarqueeManagement.FileAttachments;

public class FileAttachmentDto : EntityDto<Guid>
{
    public string FileName { get; set; }

    public string ContentType { get; set; }

    public long Size { get; set; }
    [JsonIgnore]
    public byte[] FileData { get; set; }
}