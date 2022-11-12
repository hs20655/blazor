using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    internal class CustomersConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
              builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            //builder.Property(e => e.Id).ValueGeneratedOnAdd();
            //builder.HasIndex(c => c.Id);
            //builder.Property(c => c.Name).HasMaxLength(50).IsRequired(true);
        }
    }
}
