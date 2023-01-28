using Data.DTO.Responses;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //Task<ResponseList<T>> PagedResult(int PageNumber, int PageSize, FiltersBase filtes);
    }
}
