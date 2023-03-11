using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class IpAddress : Entity
    {
       // public int Id { get; set; }      
        public string Ip { get; set; }
       // public DateTime CreatedAt { get; set; }
       // public DateTime UpdatedAt { get; set; }


        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
    }
}
