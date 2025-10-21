using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoEcommerce.Models;

namespace ProyectoEcommerce.Data
{
    public class ProyectoEcommerceContext : IdentityDbContext<IdentityUser>
    {
        public ProyectoEcommerceContext(DbContextOptions<ProyectoEcommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        // Nuevas tablas intermedias
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<BuyItem> BuyItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // IMPORTANTE para Identity

            // ShoppingCart - Productos (con cantidad)
            modelBuilder.Entity<ShoppingCartItem>()
                .HasKey(sci => new { sci.ShoppingCartId, sci.ProductId });

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(sc => sc.Items)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);

            // Buy - Productos (con cantidad y precio)
            modelBuilder.Entity<BuyItem>()
                .HasKey(bi => new { bi.BuyId, bi.ProductId });

            modelBuilder.Entity<BuyItem>()
                .HasOne(bi => bi.Buy)
                .WithMany(b => b.Items)
                .HasForeignKey(bi => bi.BuyId);

            modelBuilder.Entity<BuyItem>()
                .HasOne(bi => bi.Product)
                .WithMany()
                .HasForeignKey(bi => bi.ProductId);

            // Testimonial - Customer
            modelBuilder.Entity<Testimonial>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}