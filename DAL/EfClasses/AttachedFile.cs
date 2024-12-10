using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.EfClasses;

public class AttachedFile
{
    public int AttachedFileId { get; set; }
    public string Name { get; set; }

    [Column(TypeName = "text")]
    public FileType Type { get; set; }
    public string Key { get; set; }
    public string Url { get; set; }

    public int OwnerId { get; set; }

    [Column(TypeName = "text")]
    public AttachedFileOwnerType OwnerType { get; set; }

    [NotMapped]
    public Stream Stream { get; set; }
}

public enum FileType
{
    ImageJpeg,
    ImagePng,
    ImageGif,
}

public enum AttachedFileOwnerType
{
    User,
    Character
}