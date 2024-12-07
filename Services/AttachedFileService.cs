using DAL;
using DAL.EfClasses;

namespace Services;

public interface IAttachedFileService
{
    void PutFile(AttachedFile attachedFile);

    AttachedFile GetFile(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType, bool withStream = false);
}

internal class AttachedFileService : IAttachedFileService
{
    private readonly IUnitOfWork _uow;
    private readonly IFileStorageService _fileStorageService;

    public AttachedFileService(IFileStorageService fileStorageService, IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public void PutFile(AttachedFile attachedFile)
    {
        var (url, key) = _fileStorageService.PutFileAsync(attachedFile.Stream, attachedFile.Name, attachedFile.Type).Result;

        if (string.IsNullOrWhiteSpace(url)) return;

        attachedFile.Url = url;
        attachedFile.Key = key;
        _uow.AttachedFileRepository.InsertFile(attachedFile);
        _uow.Save();
    }

    public AttachedFile GetFile(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType, bool withStream = false)
    {
        AttachedFile attachedFile = _uow.AttachedFileRepository.GetFirstFileByOwner(ownerId, attachedFileOwnerAttachedType);

        if (attachedFile is not null && withStream)
            attachedFile.Stream = _fileStorageService.GetFileStreamAsync(attachedFile.Key, "123").Result;

        return attachedFile;
    }
}