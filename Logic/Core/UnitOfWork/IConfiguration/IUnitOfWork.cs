using Logic.Core.Repositories.IRepositories;
using Logic.Core.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.UnitOfWork.IConfiguration
{
    public interface IUnitOfWork
    {
        CustomersRepository Customers { get; }

        Task CompleteAsync(CancellationToken cancellationToken);
        
    }
}
