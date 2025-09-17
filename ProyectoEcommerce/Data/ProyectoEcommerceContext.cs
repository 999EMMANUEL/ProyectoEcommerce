using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Models;

namespace ProyectoEcommerce.Data
{
    public class ProyectoEcommerceContext : DbContext
    {
        public ProyectoEcommerceContext (DbContextOptions<ProyectoEcommerceContext> options)
            : base(options)
        {
        }


            public DbSet<Category> Categories { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Employee> Employees { get; set; }
            public DbSet<ShoppingCart> ShoppingCarts { get; set; }
            public DbSet<Buy> Buys { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Relaciones muchos-a-muchos
                modelBuilder.Entity<Buy>()
                    .HasMany(b => b.Products)
                    .WithMany(p => p.Buys)
                    .UsingEntity(j => j.ToTable("BuyProducts"));

                modelBuilder.Entity<ShoppingCart>()
                    .HasMany(sc => sc.Products)
                    .WithMany(p => p.ShoppingCarts)
                    .UsingEntity(j => j.ToTable("ShoppingCartProducts"));
            }

    }
}
