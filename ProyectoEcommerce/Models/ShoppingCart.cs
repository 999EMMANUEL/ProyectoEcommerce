using System;
using System.Collections.Generic;

namespace ProyectoEcommerce.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CustomerId { get; set; }

        // Propiedades de navegación
        public virtual Customer Customer { get; set; }

        // Relación con productos a través de tabla intermedia
        public virtual ICollection<ShoppingCartItem> Items { get; set; }
    }
}