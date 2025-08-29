using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string Apellido { get; set; }

        [Required]
        public required string Dni { get; set; }

        [Required]
        public required string Telefono { get; set; }

        public required string Email { get; set; }
    }
}
