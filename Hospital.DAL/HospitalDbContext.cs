using System;
using System.Collections.Generic;
using System.Text;
using Hospital.DAL.Domains;
using Microsoft.EntityFrameworkCore;

namespace Hospital.DAL
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> opt): base(opt)
        {
            
        }

        public DbSet<Product>  Products{ get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasIndex(u => u.Code)
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }
    }
}
