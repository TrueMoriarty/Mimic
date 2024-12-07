using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

internal class AttachedAttachedFileRepository(MimicContext context) : GenericRepository<AttachedFile>(context), IAttachedFileRepository
{
    public AttachedFile GetFirstFileByOwner(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType) =>
        context.AttachedFiles.FirstOrDefault(f =>
            f.OwnerId == ownerId
            && f.OwnerType == attachedFileOwnerAttachedType
        );

    public List<AttachedFile> GetFilesByOwner(int[] ownerIds, AttachedFileOwnerType attachedFileOwnerAttachedType) =>
        context.AttachedFiles.Where(f =>
            f.OwnerType == attachedFileOwnerAttachedType && ownerIds.Contains(f.OwnerId)
        ).ToList();

    public void InsertFile(AttachedFile attachedFile) => Insert(attachedFile);
}