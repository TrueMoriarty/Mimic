﻿using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class CharacterRepository(MimicContext context) : GenericRepository<Character>(context), ICharacterRepository
{
    public PaginatedContainer<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter)
    {
        var paginationFilter = filter.PaginateFilter;

        var query = context.Characters
            .AsNoTracking()
            .Include(c => c.Room)
            .Where(c => c.CreatorId == filter.CreatorId && (
                string.IsNullOrWhiteSpace(paginationFilter.NameFilter) || c.Name.Contains(paginationFilter.NameFilter)));

        var orderedList = paginationFilter.OrderBy switch
        {
            nameof(Character.Name) => query.OrderBy(p => p.Name),
            _ => query,
        };

        var paginatedList = orderedList
            .Skip(paginationFilter.PageIndex * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize);

        int totalCount = query.Count();

        var result = new PaginatedContainer<List<Character>>(paginatedList.ToList(),
            totalCount,
            (int) Math.Ceiling(totalCount / (double) paginationFilter.PageSize));

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