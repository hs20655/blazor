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
    internal class IpAddressConfiguration : IEntityTypeConfiguration<IpAddress>
    {
        public void Configure(EntityTypeBuilder<IpAddress> builder)
        {
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(c => c.CountryId).IsRequired(true);

            builder.Property(c => c.Ip).HasMaxLength(15).IsRequired(true);

            //builder.Property(c => c.CreatedAt).IsRequired(true);
            //builder.Property(c => c.UpdatedAt).IsRequired(true);
        }
    }
}
