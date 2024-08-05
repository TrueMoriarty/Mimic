using DAL.EfClasses;
using DAL.EfCode;

namespace DAL.Repositories;

public class UserRepository(MimicContext context) : GenericRepository<User>(context)
{
    public User? GetByOidcId(long oidcId) => context.Users.FirstOrDefault(u => u.OidcUserId == oidcId);
}