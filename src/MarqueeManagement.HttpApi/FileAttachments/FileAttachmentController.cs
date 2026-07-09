using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarqueeManagement.FileAttachments;

[Route("api/app/file-attachment")]
public class FileAttachmentController : ControllerBase
{
    private readonly IFileAttachmentAppService _fileAttachmentAppService;

    public FileAttachmentController(
        IFileAttachmentAppService fileAttachmentAppService)
    {
        _fileAttachmentAppService = fileAttachmentAppService;
    }


    [HttpPost("upload")]
    public async Task<FileAttachmentDto> UploadAsync(
     IFormFile file)
    {
        return await _fileAttachmentAppService.UploadAsync(
            file.OpenReadStream(),
            file.FileName,
            file.ContentType,
            file.Length
        );
    }
}