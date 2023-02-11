using Abp.Linq.Expressions;
using Azure;
using Data;
using Data.DTO.Responses;
using Data.Entities;
using Data.Shared;
using Logic.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Repositories.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected ModelContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public GenericRepository(ModelContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;

        }

        public async Task<T?> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<bool> Add(T entity, CancellationToken cancellationToken)
        {
            //BASE ADD (if need more actions need to override this methods in concrete repository)
            try
            {
                await dbSet.AddAsync(entity, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add function error", typeof(T));
                return false;
            }
            
            
        }
        public virtual async Task<bool> Delete(Guid id)
        {
            //BASE DELETE (if need more actions need to override this methods in concrete repository)
            try
            {
                var exist = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist); return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(T));
                return false;
            }
        }
        public virtual bool Update(T entity)
        {
            //BASE UPDATE (if need more actions need to override this methods in concrete repository)
            try
            {
                dbSet.Attach(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(T));
                return false;
            }
        }

        public async Task<PagingResult<T>> Paging(Paging request)
        {
            var result = await dbSet.Paging(request.SearchQuery, request.PageNumber, request.PageSize)
                .AsNoTracking().ToListAsync();

            if (request.PageNumber == 0)
            {
                var total = await dbSet.SearchQuery(request.SearchQuery).CountAsync();
                return new PagingResult<T>
                {
                    Timestamp = DateTime.Now,
                    Result = new List<T>(result),
                    Total = total
                };
            }

            return new PagingResult<T>
            {
                Timestamp = default(DateTime?),
                Result = new List<T>(result),
                Total = default(int?)
            };
        }

        public async Task<int> TotalRecords()
        {
            return await dbSet.CountAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
