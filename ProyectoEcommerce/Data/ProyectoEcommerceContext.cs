using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoEcommerce.Data
{
    public class ProyectoEcommerceContext
       : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ProyectoEcommerceContext(DbContextOptions<ProyectoEcommerceContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Faq> Faqs { get; set; }
       // public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 👇 Esto registra TODAS las entidades de Identity, incluyendo IdentityRole
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

            //índice por categoría para ordenar/filtrar rápido
            modelBuilder.Entity<Faq>()
                .HasIndex(f => new { f.Category, f.SortOrder });

            base.OnModelCreating(modelBuilder); //  para Identity
        }

    }
}
