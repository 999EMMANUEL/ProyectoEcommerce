using System.ComponentModel.DataAnnotations;

namespace ProyectoEcommerce.Models
{
    public class Buy
    {
        public int BuyId { get; set; }

        [Required(ErrorMessage = "El ID del carro es obligatorio")]
        public int ShoppingCartId { get; set; }
        [Required(ErrorMessage = "El ID del Cliente es obligatorio")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "El ID del empleado es obligatorio")]
        public int EmployeeId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public virtual List<ShoppingCart> ShoppingCarts { get; set; }
        public virtual Customer Customers { get; set; }
        public virtual Employee Employees { get; set; }
    }
}
