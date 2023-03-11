using Data.DTO.Responses.IpInfo;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Core.Repositories.IRepositories
{
    public interface IIpAddressesRepository
    {
        public Task<IpInfoResponse?> GetCountryDataByIp(string ip);
    }
}
