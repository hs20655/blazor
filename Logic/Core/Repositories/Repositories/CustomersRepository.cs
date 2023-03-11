using Data;
using Data.Entities;
using Logic.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Repositories.Repositories
{
    public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
    {
       public CustomersRepository(ModelContext context, ILogger logger) : base(context, logger) {}

        public int GetTopcutomers(int a)
        {
            //just example  Write her your methods with diffrent fvalidations
           // dbSet.Add(new Customer());
            //return await dbSet
            //   .Include(i => i.Invoices)
            //   //.Include(p => p.Parkings)
            //   //.Include(v => v.ClientVehicles) //NEED TO CHECK 
            //   .ToListAsync();
            
            return 1;
        }
    }
}
