using DAL.EfClasses;
using DAL.EfCode;

namespace DAL.Repositories;

public class UserRepository(MimicContext context) : GenericRepository<User>(context)
{
    public User? GetByExternalId(string externalId) => context.Users.FirstOrDefault(u => u.ExternalUserId == externalId);
}