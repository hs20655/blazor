using Data;
using Data.Entities;
using Logic.Core.UnitOfWork.IConfiguration;
using Microsoft.Extensions.Caching.Memory;

namespace Api.BackgroundServices
{
    public class IpInfoService : BackgroundService
    {
        private const int generalDelay = 1 * 10 * 1000; // 10 seconds NEED TO ADD THIS TO appsettings.json ModelContext context
        protected IMemoryCache _memoryCache;
        protected IHttpClientFactory _clientFactory;
        const int BUTCH_TOTAL_RECORDS = 100;
        public IpInfoService(IMemoryCache memoryCache, IHttpClientFactory clientFactory)
        {
            _memoryCache = memoryCache;
            _clientFactory = clientFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(generalDelay, cancellationToken);
                await SyncDatabase();
            }
        }

        private static Task SyncDatabase()
        {
            using(ModelContext context = new ModelContext())
            {
                //int totalItems = context.Set<IpAddress>().Count();
                
                for (int i = 0; i < context.Set<IpAddress>().Count(); i+= BUTCH_TOTAL_RECORDS)
                {
                    // call remote api to check each ip address in table if still exist and has same data == ip belongs to same country
                    // if not, update database and cash
                }

            }


            return Task.FromResult("Done");
        }

    }
}
