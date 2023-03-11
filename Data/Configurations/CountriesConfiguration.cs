using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    internal class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired(true);
            builder.Property(c => c.TwoLetterCode).HasMaxLength(2).IsRequired(true);
            builder.Property(c => c.ThreeLetterCode).HasMaxLength(3).IsRequired(true);
            //builder.Property(c => c.CreatedAt).IsRequired(true);
            //ena pros polla
            builder.HasMany(ip => ip.IpAddresses).WithOne(c => c.Country);
        }
    }
}
