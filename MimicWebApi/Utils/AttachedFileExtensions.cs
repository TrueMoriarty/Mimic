using Amazon.S3.Model;
using DAL.EfClasses;

namespace MimicWebApi.Utils;

public static class AttachedFileExtensions
{
    public static AttachedFile CreateAttachedFile(this IFormFile formFile)
    {
        MemoryStream stream = new();
        formFile.CopyTo(stream);
        stream.Position = 0;

        AttachedFile attachedFile = new()
        {
            Stream = stream,
            Name = formFile.FileName,
            Type = formFile.ContentType switch
            {
                "image/jpeg" => FileType.ImageJpeg,
                "image/png" => FileType.ImagePng,
                "image/gif" => FileType.ImageGif,
                _ => throw new ArgumentOutOfRangeException()
            }
        };

        return attachedFile;
    }

    public static AttachedFile CreateAttachedFile(this IFormFile formFile, int ownerId, AttachedFileOwnerType fileOwnerType)
    {
        AttachedFile attachedFile = formFile.CreateAttachedFile();
        attachedFile.OwnerId = ownerId;
        attachedFile.OwnerType = fileOwnerType;

        return attachedFile;
    }
}