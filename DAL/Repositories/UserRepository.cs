using DAL.EfClasses;
using DAL.EfCode;

namespace DAL.Repositories;

public class UserRepository(MimicContext context) : GenericRepository<User>(context)
{
    public bool Any(Func<User, bool> predicate) => context.Users.Any(predicate);
    public User? GetByOidcId(long oidcId) => context.Users.FirstOrDefault(u=>u.OidcUserId == oidcId);
}