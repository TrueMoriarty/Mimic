using DAL;
using DAL.EfClasses;

namespace Services;

public interface IUsersService
{
    public void Add(User user);

    public User? GetByExternalId(string externalId);
    public User? GetById(int id);
}

public class UsersService(IUnitOfWork unitOfWork) : IUsersService
{
    public void Add(User user)
    {
        unitOfWork.UserRepository.AddUser(user);
        unitOfWork.Save();
    }

    public User? GetByExternalId(string externalId) => unitOfWork.UserRepository.GetByExternalId(externalId);

    public User GetById(int id)
    {
        User user = unitOfWork.UserRepository.GetById(id);
        if (user is not null)
            user.Icon = unitOfWork.AttachedFileRepository.GetFirstFileByOwner(user.UserId, AttachedFileOwnerType.User);
        return user;
    }
}