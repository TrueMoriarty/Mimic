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

    public void InsertFile(AttachedFile attachedFile) => Insert(attachedFile);
}