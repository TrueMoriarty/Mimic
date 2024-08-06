using DAL;
using DAL.EfClasses;

namespace Services;

public interface IUserService
{
    public void Add(User user);

    public User? GetByExternalId(string externalId);
    public User? GetById(int id);
}

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public void Add(User user)
    {
        unitOfWork.UserRepository.Insert(user);
        unitOfWork.Save();
    }

    public User? GetByExternalId(string externalId) => unitOfWork.UserRepository.GetByExternalId(externalId);
    public User GetById(int id) => unitOfWork.UserRepository.GetByID(id);
}