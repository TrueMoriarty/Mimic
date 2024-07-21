using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories;

namespace DAL;

public class UnitOfWork : IDisposable
{
    // Создаются переменные класса для контекста бд и каждого репозитория.
    // Для переменной context создается новый контекст.
    private MimicContext context = new MimicContext();
    GenericRepository<User> userRepository;
    GenericRepository<Item> itemRepository;

    private bool disposed = false;

    // Каждое свойство репозитория проверяет существует ли репозиторий. Если нет
    // создается экземпляр репозитория и ему передается контекст. Поэтому все репозитории
    // используют один и тот же экземпляр контекста
    public GenericRepository<User> UserRepository 
    {
        get
        {
            if (userRepository == null)
                userRepository = new GenericRepository<User>(context);
            return userRepository;
        }
    }
    public GenericRepository<Item> ItemRepository
    {
        get
        {
            if (itemRepository == null)
                itemRepository = new GenericRepository<Item>(context);
            return itemRepository;
        }
    }

    public void Save()
    {
        context.SaveChanges();
    }

    // реализация интерфейса IDisposable и удаление контекста
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                context.Dispose();
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
