using Microsoft.AspNetCore.Mvc;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController(IFileService fileService) : ControllerBase
{
    [HttpPost("{bucket}")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile formFile, [FromRoute] string bucket)
    {
        MemoryStream stream = new MemoryStream();
        formFile.CopyTo(stream);
        stream.Position = 0;

        await fileService.PutFileAsync(stream, formFile.FileName, formFile.ContentType, bucket);
        return Ok();
    }

    [HttpGet("{namefile}")]
    public async Task<IActionResult> GetFile([FromRoute] string namefile)
    {
        var res = await fileService.GetFileAsync(namefile);

        return File(res, $"image/jpeg", $"{namefile}");
    }
}