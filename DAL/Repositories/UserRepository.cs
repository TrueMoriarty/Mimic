using DAL.EfClasses;
using DAL.EfCode;

namespace DAL.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    public User? GetByExternalId(string externalId);
}

public class UserRepository(MimicContext context) : GenericRepository<User>(context), IUserRepository
{
    public User? GetByExternalId(string externalId) => context.Users.FirstOrDefault(u => u.ExternalUserId == externalId);
}