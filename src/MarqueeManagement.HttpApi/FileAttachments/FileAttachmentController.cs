using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.FileAttachments;

[Route("api/app/file-attachment")]
public class FileAttachmentController : AbpController
{
    private readonly IFileAttachmentAppService _fileAttachmentAppService;

    public FileAttachmentController(IFileAttachmentAppService fileAttachmentAppService)
    {
        _fileAttachmentAppService = fileAttachmentAppService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<FileAttachmentDto> UploadAsync([FromForm] IFormFile file)
    {
        return await _fileAttachmentAppService.UploadAsync(
            file.OpenReadStream(),
            file.FileName,
            file.ContentType,
            file.Length
        );
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadAsync(Guid id)
    {
        var file = await _fileAttachmentAppService.GetAsync(id);
        return File(file.FileData, file.ContentType, file.FileName);
    }
}