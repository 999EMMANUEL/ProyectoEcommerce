using System.ComponentModel.DataAnnotations;

namespace ProyectoEcommerce.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string Name_full { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required, EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Número no válido")]
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public virtual ICollection<Buy> Buys { get; set; }
    }
}
