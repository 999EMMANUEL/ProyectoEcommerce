using System.ComponentModel.DataAnnotations;

namespace ProyectoEcommerce.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        [Required, EmailAddress(ErrorMessage = "Correo inválido")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Número no válido")]
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public virtual List<Buy> Buys { get; set; }
    }
}
