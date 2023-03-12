using Abp.Extensions;
using Dapper;
using Data.DbQueryObjects;
using Data.DTO.Requests.IpInfo;
using Data.DTO.Responses.IpInfo;
using Logic.WorkFlow.Queries;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.WorkFlow.QueryHandlers.IpInfo
{
    public sealed class TwoLetterReportQueryHandler : IRequestHandler<ReportByTwoLetterQuery, List<TwoLetterReportDbQueryResponse>>
    {
        public TwoLetterReportQueryHandler()
        {
        }

        public async Task<List<TwoLetterReportDbQueryResponse>> Handle(ReportByTwoLetterQuery query, CancellationToken cancellationToken)
        {
            var result = new List<TwoLetterReportDbQueryResponse>();
            //select Countries.Name as CountryName, Count(IpAddresses.Ip) as AddressesCount, MAX(IpAddresses.CreatedDate) AS LastAddressUpdated from Countries
            //join IpAddresses ON Countries.Id = IpAddresses.CountryId group by Countries.Name
            using (IDbConnection db = new SqlConnection(@"Server=(localdb)\MSSQLLOcalDB;Database=BLAZOR_USERS;Trusted_Connection=True;")) // get connection string form appsettings.json
            {
                string sqlQuery = string.Empty;
                string queryPart1 = "select Countries.Name as CountryName, Count(IpAddresses.Ip) as AddressesCount, cast(MAX(IpAddresses.CreatedDate) as varchar) AS LastAddressUpdated from Countries join IpAddresses ON Countries.Id = IpAddresses.CountryId ";
                string queryPart2 = "where Countries.TwoLetterCode ='" + query.TwoLetter + "' ";
                string queryPart3 = "group by Countries.Name";

                if (!query.TwoLetter.IsNullOrEmpty())
                {
                    sqlQuery = queryPart1 + queryPart2 + queryPart3;
                }
                else 
                {
                    sqlQuery = queryPart1  + queryPart3;
                }
                
                result =  await db.QueryAsync<TwoLetterReportDbQueryResponse>(sqlQuery) as List<TwoLetterReportDbQueryResponse>;

                
            }

            return result;
        }
    }
}
