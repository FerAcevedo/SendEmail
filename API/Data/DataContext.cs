using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SLA>().HasData(
                new SLA { Id = 1, Description ="Customer"},
                new SLA {Id =2, Description="Internal"},
                new SLA {Id = 3, Description="Multilevel"}
            );
        }

        public DbSet<EmailInfo> Emails { get; set; }

        public DbSet<SLA> ServicesLA { get; set; }
    }
}