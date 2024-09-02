using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class CharacterRepository(MimicContext context) : GenericRepository<Character>(context), ICharacterRepository
{
    public PaginatedContainerDto<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter)
    {
        var query = context.Characters
            .AsNoTracking()
            .Include(c => c.Room)
            .Where(c => filter.CreatorId == null || c.CreatorId == filter.CreatorId);

        var paginatedList = query
            .OrderByDescending(c => c.CreateDate)
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize);

        int totalCount = query.Count();

        var result = new PaginatedContainerDto<List<Character>>(paginatedList.ToList(),
            totalCount,
            (int) Math.Ceiling(totalCount / (double) filter.PageSize));

        return result;
    }

    public Character GetById(int id, bool readOnly) =>
        Get(
                character => character.CharacterId == id,
                includeProperties: "Room,Storage.Items.Properties",
                readOnly: readOnly
            )
            .FirstOrDefault();

}