using DAL.EfClasses;

namespace DAL.Repositories.Interfaces;

public interface ICharacterRepository
{
    List<Character> GetListByCreatorId(int creatorId);
    Character GetById(int id);
}