using Data.Authentication;
using Data.Configurations;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<IpAddress> IpAddresses { get; set; }
        

        public ModelContext()
        {
           // Database.EnsureDeleted(); //add to appsetting.json
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;
            optionsBuilder.UseSqlServer(
            @"Server=localhost\SQLEXPRESS;Database=BLAZOR_USERS;Trusted_Connection=True;TrustServerCertificate=True"); //add to appsetting.json
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            //ADD configurations for entities
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());
            modelBuilder.ApplyConfiguration(new IpAddressConfiguration());

        }

    }
}
