using Microsoft.Identity.Client;

namespace ProyectoEcommerce.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public DateTime CreatedDate { get; set; }

        // FK
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        // Muchos a muchos con Product
        public virtual ICollection<Product> Products { get; set; }
    }
}