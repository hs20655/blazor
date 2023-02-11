using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
