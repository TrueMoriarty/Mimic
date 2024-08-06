using DAL;
using DAL.EfClasses;
using DAL.Repositories;

namespace MimicBLTests.Mocks;

internal class MockUnitOfWork : IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IGenericRepository<Item> ItemRepository { get; }
    public void Save()
    {
        throw new NotImplementedException();
    }
}
