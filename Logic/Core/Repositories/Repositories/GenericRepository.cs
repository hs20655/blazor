using Azure;
using Data;
using Data.DTO.Responses;
using Data.Entities;
using Logic.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        public async Task<ResponseList<T>> PagedResult(int PageNumber, int PageSize)
        {
            // this method can page/filter/sort  - to implement
            return new ResponseList<T>() 
            {
                // dennd to pas predecate in order to seard
                //await dbSet.Where<F>(new Predicate).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync()
                Data = await dbSet.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync(), 
                TotalRecords = await dbSet.CountAsync()
            };
        }
    }
}
