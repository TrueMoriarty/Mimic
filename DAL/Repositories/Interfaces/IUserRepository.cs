using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface IUserRepository : IDisposable
{
    User? GetByExternalId(string externalId);
    User? GetById(int id);

    void AddUser(User user);
}