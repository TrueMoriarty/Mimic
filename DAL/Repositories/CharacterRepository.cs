using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class CharacterRepository(MimicContext context) : GenericRepository<Character>(context), ICharacterRepository
{
    public PaginatedContainer<List<Character>> GetPaginatedListByCreatorId(int creatorId, PaginateDataItemDto paginateDataItem)
    {
        var query = context.Characters
            .Include(c => c.Room)
            .Where(c => c.CreatorId == creatorId && (
                string.IsNullOrWhiteSpace(paginateDataItem.NameFilter) || c.Name.Contains(paginateDataItem.NameFilter)));

        var orderedList = paginateDataItem.OrderBy switch
        {
            nameof(Character.Name) => query.OrderBy(p => p.Name),
            _ => query,
        };

        var paginatedList = orderedList
            .Skip(paginateDataItem.PageIndex * paginateDataItem.PageSize)
            .Take(paginateDataItem.PageSize);

        int totalCount = query.Count();

        var result = new PaginatedContainer<List<Character>>(paginatedList.ToList(),
            totalCount,
            (int) Math.Ceiling(totalCount / (double) paginateDataItem.PageSize));

        return result;
    }

    public Character GetById(int id) =>
        Get(
                character => character.CharacterId == id,
                includeProperties: "Room,Storage.Items.Properties",
                readOnly: true
            )
            .FirstOrDefault();
}