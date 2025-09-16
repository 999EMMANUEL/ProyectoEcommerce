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

        public DbSet<Product> Product { get; set; }

        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Buy> Buy { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

    }
}
