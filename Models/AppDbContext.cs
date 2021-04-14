using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace T11ASP.NetProject.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasKey(c => new { c.OrderId, c.ProductId});
            
            modelBuilder.Entity<Cart>()
                 .HasKey(c => new { c.CartId});
            
            modelBuilder.Entity<CartDetails>()
                 .HasKey(c => new { c.CartId,c.ProductId});

            modelBuilder.Entity<ProductComment>()
                 .HasKey(c => new { c.ProductId,c.CustomerId,c.OrderId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }


        public DbSet<ProductList> ProductList { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Customer> Customer { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<ProductComment> ProductComment { get; set; }

    }
}
