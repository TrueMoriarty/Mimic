using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories;

namespace DAL;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IGenericRepository<Item> ItemRepository { get; }

    public void Save();
}

public class UnitOfWork : IUnitOfWork, IDisposable
{
    // Создаются переменные класса для контекста бд и каждого репозитория.
    // Для переменной context создается новый контекст.
    private MimicContext context = new MimicContext();
    UserRepository? userRepository;
    GenericRepository<Item>? itemRepository;

    private bool disposed = false;

    // Каждое свойство репозитория проверяет существует ли репозиторий. Если нет
    // создается экземпляр репозитория и ему передается контекст. Поэтому все репозитории
    // используют один и тот же экземпляр контекста
    public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
    public IGenericRepository<Item> ItemRepository => itemRepository ??= new GenericRepository<Item>(context);

    public void Save()
    {
        context.SaveChanges();
    }

    // реализация интерфейса IDisposable и удаление контекста
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
            context.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}