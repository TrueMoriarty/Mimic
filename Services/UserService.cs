using DAL;
using DAL.EfClasses;

namespace Services;

public interface IUserService
{
    public void Add(User user);

    public User? GetByOidcId(long oidcId);
    public User? GetById(int id);
}

public class UserService(UnitOfWork unitOfWork) : IUserService
{
    public void Add(User user)
    {
        unitOfWork.UserRepository.Insert(user);
        unitOfWork.Save();
    }

    public User? GetByOidcId(long oidcId) => unitOfWork.UserRepository.GetByOidcId(oidcId);
    public User GetById(int id) => unitOfWork.UserRepository.GetByID(id);
}