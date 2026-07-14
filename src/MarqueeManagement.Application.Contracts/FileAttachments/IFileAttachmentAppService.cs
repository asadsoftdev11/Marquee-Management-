using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MarqueeManagement.FileAttachments;

public interface IFileAttachmentAppService : IApplicationService
{
    Task<FileAttachmentDto> GetAsync(Guid id);
    Task<byte[]> GetFileBytesAsync(Guid id);
    Task<FileAttachmentDto> UploadAsync(
        Stream stream,
        string fileName,
        string contentType,
        long size);
}