using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Shared
{
    public static class SearchQueryExtension
    {
        public static IQueryable<T> Paging<T>(this DbSet<T> entity,
           SearchQuery searchQuery, int pageNumber, int pageSize)
           where T : Entity
        {
            return entity.SearchQuery(searchQuery)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> SearchQuery<T>(this DbSet<T> entity,
            SearchQuery searchQuery)
            where T : Entity
        {
            return (searchQuery != null ? searchQuery
                .ToQuery(entity) : entity);
        }
    }
}
