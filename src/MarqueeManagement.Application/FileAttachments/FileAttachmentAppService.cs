using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.FileAttachments;

[RemoteService(IsEnabled = false)]
public class FileAttachmentAppService : ApplicationService, IFileAttachmentAppService
{
    private readonly IRepository<FileAttachment, Guid> _repository;
    private readonly ICurrentTenant _currentTenant;

    public FileAttachmentAppService(
        IRepository<FileAttachment, Guid> repository,
        ICurrentTenant currentTenant)
    {
        _repository = repository;
        _currentTenant = currentTenant;
    }

    public async Task<FileAttachmentDto> GetAsync(Guid id)
    {
        var file = await _repository.GetAsync(id);
        return ObjectMapper.Map<FileAttachment, FileAttachmentDto>(file);
    }

    public async Task<byte[]> GetFileBytesAsync(Guid id)
    {
        var file = await _repository.GetAsync(id);
        return file.FileData; 
    }

    public async Task<FileAttachmentDto> UploadAsync(
        Stream stream, string fileName, string contentType, long size)
    {
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        var attachment = new FileAttachment(
            GuidGenerator.Create(),
            _currentTenant.Id,
            fileName,
            Guid.NewGuid().ToString(),
            contentType,
            size,
            fileBytes
        );

        await _repository.InsertAsync(attachment);
        return ObjectMapper.Map<FileAttachment, FileAttachmentDto>(attachment);
    }
}