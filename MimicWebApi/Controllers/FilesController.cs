using DAL.EfClasses;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MimicWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController(IAttachedFileService attachedFileService) : ControllerBase
{
    [HttpPost]
    public  IActionResult UploadFile([FromForm] IFormFile formFile,
        [FromForm] int ownerId,
        [FromForm] AttachedFileOwnerType fileOwnerType)
    {
        MemoryStream stream = new();
        formFile.CopyTo(stream);
        stream.Position = 0;

        AttachedFile attachedFile = new()
        {
            OwnerId = ownerId,
            OwnerType = fileOwnerType,
            Stream = stream,
            Name = formFile.FileName,
            Type = formFile.ContentType switch
            {
                "image/jpeg" => FileType.ImageJpeg,
                "image/png" => FileType.ImagePng,
                _ => throw new ArgumentOutOfRangeException()
            }
        };

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