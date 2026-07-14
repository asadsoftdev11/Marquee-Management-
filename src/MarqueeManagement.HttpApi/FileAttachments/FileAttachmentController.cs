using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace MarqueeManagement.FileAttachments;

[ApiVersion("1.0")]
[ControllerName("FileAttachments")]
[Area("app")]
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
    public async Task<Guid> UploadAsync([FromForm] IFormFile file)
    {
        var result = await _fileAttachmentAppService.UploadAsync(
            file.OpenReadStream(),
            file.FileName,
            file.ContentType,
            file.Length
        );
        return result.Id;
    }

    [HttpGet("download/{id}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> DownloadAsync(Guid id)
    {
        var fileBytes = await _fileAttachmentAppService.GetFileBytesAsync(id);
        var metadata = await _fileAttachmentAppService.GetAsync(id);

        return File(fileBytes, metadata.ContentType, metadata.FileName);
    }
}