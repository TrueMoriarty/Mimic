﻿using DAL.Dto;
using DAL.EfClasses;
using DAL.EfCode;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

internal class CharacterRepository(MimicContext context) : GenericRepository<Character>(context), ICharacterRepository
{
    public PaginatedContainerDto<List<Character>> GetPaginatedListByCreatorId(CharacterFilter filter)
    {
        var paginationFilter = filter.Pagination;

        var query = context.Characters
            .AsNoTracking()
            .Include(c => c.Room)
            .Where(c => c.CreatorId == filter.CreatorId);


        var paginatedList = query
            .OrderByDescending(c => c.CreateDate)
            .Skip(paginationFilter.PageIndex * paginationFilter.PageSize)
            .Take(paginationFilter.PageSize);

        int totalCount = query.Count();

        var result = new PaginatedContainerDto<List<Character>>(paginatedList.ToList(),
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

    public Character AddCharacter(Character character)
    {
        Insert(character);
        return character;
    }
}