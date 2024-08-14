using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

internal class UserRepository(MimicContext context) : GenericRepository<User>(context), IUserRepository
{
    public User? GetByExternalId(string externalId) => context.Users.FirstOrDefault(u => u.ExternalUserId == externalId);
    public User? GetById(int id) => GetByID(id);
    public void AddUser(User user) => Insert(user);
}