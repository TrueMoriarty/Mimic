using DAL;
using DAL.EfClasses;

namespace Services;

public interface IAttachedFileService
{
    void PutFile(AttachedFile attachedFile);
    AttachedFile GetFile(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType, bool withStream = false);
    void EditFile(AttachedFile attachedFile);
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
        string key = $"{Guid.NewGuid()}";
        string url = _fileStorageService.PutFileAsync(attachedFile.Stream, key, attachedFile.Type).Result;
        if (string.IsNullOrWhiteSpace(url)) return;

        attachedFile.Url = url;
        attachedFile.Key = key;
        _uow.AttachedFileRepository.InsertFile(attachedFile);
        _uow.Save();
    }

    public void EditFile(AttachedFile attachedFile)
    {
        _fileStorageService.DeleteFileAsync(attachedFile.Key).Wait();

        string key = $"{Guid.NewGuid()}";
        string url = _fileStorageService.PutFileAsync(attachedFile.Stream, key, attachedFile.Type).Result;
        if (string.IsNullOrWhiteSpace(url)) return;

        attachedFile.Url = url;
        attachedFile.Key = key;
        _uow.AttachedFileRepository.UpdateFile(attachedFile);
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