using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Apellido { get; set; }

        [Required]
        public required string Dni { get; set; }

        [Required]
        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}";
        }
    }
}
