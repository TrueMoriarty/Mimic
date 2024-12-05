﻿namespace DAL.Repositories.Interfaces;

using EfClasses;

public interface IAttachedFileRepository
{
    AttachedFile GetFirstFileByOwner(int ownerId, AttachedFileOwnerType attachedFileOwnerAttachedType);
    void InsertFile(AttachedFile attachedFile);
}