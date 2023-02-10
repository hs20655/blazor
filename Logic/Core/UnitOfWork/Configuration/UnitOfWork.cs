using Data;
using Logic.Core.Repositories.IRepositories;
using Logic.Core.Repositories.Repositories;
using Logic.Core.UnitOfWork.IConfiguration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.UnitOfWork.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ModelContext _context;
        private readonly ILogger _logger;

        public CustomersRepository Customers { get; private set; }

        public UnitOfWork(ModelContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Customers  = new CustomersRepository(_context, _logger);

        }
        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void DisposeDbConnection()
        {
            Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
