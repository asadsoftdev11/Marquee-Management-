using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.FileAttachments;

public interface IFileAttachmentAppService :
    ICrudAppService<
        FileAttachmentDto,
        Guid,
        Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto,
        CreateFileAttachmentDto>
{
    Task<FileAttachmentDto> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        long size);
}