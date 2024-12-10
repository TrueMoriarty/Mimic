using DAL.EfClasses;
using Microsoft.AspNetCore.Mvc;
using MimicWebApi.Utils;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController(IAttachedFileService attachedFileService) : ControllerBase
{
    [HttpPost]
    public IActionResult UploadFile([FromForm] IFormFile formFile,
        [FromForm] int ownerId,
        [FromForm] AttachedFileOwnerType fileOwnerType)
    {
        AttachedFile attachedFile = formFile.MapToAttachedFile(ownerId, fileOwnerType);

        attachedFileService.PutFile(attachedFile);
        return Ok(attachedFile.Url);
    }

    [HttpGet("{ownerId}")]
    public IActionResult GetFile([FromRoute] int ownerId)
    {
        var res = attachedFileService.GetFile(1,
            AttachedFileOwnerType.User,
            true);

        return File(res.Stream, $"image/jpeg", $"{res.Name}");
    }
}