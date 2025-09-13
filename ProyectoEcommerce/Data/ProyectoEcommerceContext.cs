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
    }
}
