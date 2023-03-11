using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Country : Entity
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        //public DateTime CreatedAt { get; set; }

        public ICollection<IpAddress> IpAddresses { get; set; }
    }
}
