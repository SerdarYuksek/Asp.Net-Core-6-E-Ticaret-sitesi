using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= SEDO;database=TicaretSiteDb; integrated security=true; TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
                .ToTable(tb => tb.HasTrigger("StokAzalt"));
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
