using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IItemRepository ItemRepository { get; }
    IGenericRepository<Storage> StorageRepository { get; }
    IGenericRepository<ItemProperty> PropertiesRepository { get; }
    ICharacterRepository CharactersRepository { get; }
    IAttachedFileRepository AttachedFileRepository { get; }
    IRoomRepository RoomRepository { get; }

    void Save();
}

public class UnitOfWork : IUnitOfWork, IDisposable
{
    // Создаются переменные класса для контекста бд и каждого репозитория.
    // Для переменной context создается новый контекст.
    private MimicContext context = new();

    UserRepository? userRepository;
    ItemRepository? itemRepository;
    GenericRepository<Storage>? storageRepository;
    GenericRepository<ItemProperty>? propertyRepository;
    ICharacterRepository? characterRepository;
    AttachedAttachedFileRepository? fileRepository;
    RoomRepository? roomRepository;

    private bool disposed = false;

    // Каждое свойство репозитория проверяет существует ли репозиторий. Если нет
    // создается экземпляр репозитория и ему передается контекст. Поэтому все репозитории
    // используют один и тот же экземпляр контекста
    public IUserRepository UserRepository =>
        userRepository ??= new UserRepository(context);

    public IItemRepository ItemRepository =>
        itemRepository ??= new ItemRepository(context);

    public IGenericRepository<Storage> StorageRepository =>
        storageRepository ??= new GenericRepository<Storage>(context);

    public IGenericRepository<ItemProperty> PropertiesRepository =>
        propertyRepository ??= new GenericRepository<ItemProperty>(context);

    public ICharacterRepository CharactersRepository =>
        characterRepository ??= new CharacterRepository(context);

    public IAttachedFileRepository AttachedFileRepository =>
        fileRepository ??= new AttachedAttachedFileRepository(context);

    public IRoomRepository RoomRepository =>
        roomRepository ??= new RoomRepository(context);

    public void Save()
    {
        context.SaveChanges();
    }

    // реализация интерфейса IDisposable и удаление контекста
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;

        if (disposing)
        {
            context.Dispose();
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}