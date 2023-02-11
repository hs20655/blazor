using Data.DTO.Responses;
using Data.Entities;
using Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T?> GetById(Guid id);
        Task<bool> Add(T entity, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id);
        bool Update(T entity);
        Task<PagingResult<T>> Paging(Paging request);
        Task<int> TotalRecords();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
