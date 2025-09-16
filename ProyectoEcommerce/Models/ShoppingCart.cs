using Microsoft.Identity.Client;

namespace ProyectoEcommerce.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int precioUnitario { get; set; }
        public int cantidad { get; set; }
        public int descuento { get; set; }
        public virtual Product Products { get; set; }
        public virtual Buy buys { get; set; }
    }
}
