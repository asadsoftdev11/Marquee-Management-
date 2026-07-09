using System;
using System.IO;
using Volo.Abp.ObjectMapping;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.MultiTenancy;

namespace MarqueeManagement.FileAttachments;

public class FileAttachmentAppService :
    CrudAppService<
        FileAttachment,
        FileAttachmentDto,
        Guid,
        Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto,
        CreateFileAttachmentDto>,
    IFileAttachmentAppService
{
    private readonly IRepository<FileAttachment, Guid> _fileAttachmentRepository;
    private readonly ICurrentTenant _currentTenant;

    public FileAttachmentAppService(
     IRepository<FileAttachment, Guid> fileAttachmentRepository,
     ICurrentTenant currentTenant)
     : base(fileAttachmentRepository)
    {
        _fileAttachmentRepository = fileAttachmentRepository;
        _currentTenant = currentTenant;
    }

    public async Task<FileAttachmentDto> UploadAsync(
    Stream stream,
    string fileName,
    string contentType,
    long size)
    {
        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            MarqueeManagementConsts.FileUploadPath
        );


        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }


        var uniqueFileName =
            Guid.NewGuid() + Path.GetExtension(fileName);


        var filePath = Path.Combine(
            uploadsFolder,
            uniqueFileName
        );


        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await stream.CopyToAsync(fileStream);
        }


        var attachment = new FileAttachment(
            GuidGenerator.Create(),
            _currentTenant.Id,
            fileName,
            uniqueFileName,
            contentType,
            size
        );


        await _fileAttachmentRepository.InsertAsync(
            attachment
        );


        return ObjectMapper.Map<FileAttachment, FileAttachmentDto>(
            attachment
        );
    }

}