namespace DAL.Repositories.Interfaces;

using EfClasses;

public interface IAttachedFileRepository : IGenericRepository<AttachedFile>
{
    AttachedFile GetFirstFileByOwner(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType);
    void InsertFile(AttachedFile attachedFile);
    void UpdateFile(AttachedFile attachedFile);
    List<AttachedFile> GetFilesByOwner(int[] ownerIds, AttachedFileOwnerType attachedFileOwnerAttachedType);
}