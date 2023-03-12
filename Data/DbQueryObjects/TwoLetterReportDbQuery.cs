using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DbQueryObjects
{
    public class TwoLetterReportDbQueryResponse
    {
        public string CountryName { get; set; }
        public int AddressesCount { get; set; }
        
        public string LastAddressUpdated { get; set; }
    }
}
