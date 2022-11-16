﻿using Data.Configurations;
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
    //public class ModelContext : DbContext
    //{
    //    public DbSet<Customer> Customers { get; set; }

    //    public ModelContext()
    //    {
    //       // Database.EnsureDeleted();
    //      // Database.EnsureCreated();
    //    }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer(
    //        @"Server=(localdb)\MSSQLLOcalDB;Database=BLAZOR;Trusted_Connection=True;");
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {

    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.ApplyConfiguration(new CustomersConfiguration());

    //    }

    //}

    //IdentityDbContext and not DbContext (need identity tables)
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }

        public ModelContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLOcalDB;Database=BLAZOR;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());

        }

    }
}
