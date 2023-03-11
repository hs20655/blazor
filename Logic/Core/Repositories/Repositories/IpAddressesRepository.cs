using Data;
using Data.DTO.Responses.IpInfo;
using Data.Entities;
using Logic.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Repositories.Repositories
{
    public class IpAddressesRepository : GenericRepository<IpAddress>, IIpAddressesRepository
    {
        public IpAddressesRepository(ModelContext context, ILogger logger) : base(context, logger){}

        public async Task<IpInfoResponse?> GetCountryDataByIp(string ip)
        {
            var result = await dbSet.Include(i => i.Country).FirstOrDefaultAsync(i => i.Ip == ip);
            if(result != null && result.Country != null)
            {
                return new IpInfoResponse()
                {
                    CountryName = result.Country.Name,
                    ThreeLetterCode = result.Country.ThreeLetterCode,
                    TwoLetterCode = result.Country.TwoLetterCode,
                };
            }
           return null;
        }
    }
}
