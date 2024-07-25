using DAL;
using DAL.EfClasses;
using DAL.Repositories;

namespace Services;

public interface IUserService
{
    public void Add(User user);

    public User? GetByOidcId(long oidcId);
}

public class UserService(UnitOfWork unitOfWork) : IUserService
{

    public void Add(User user)
    {
        unitOfWork.UserRepository.Insert(user);
        unitOfWork.Save();
    }

    public User? GetByOidcId(long oidcId) => unitOfWork.UserRepository.GetByOidcId(oidcId);
}