using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        public int Id { get; set; }

        [Required]
        public string? Direccion { get; set; }

        [Required]
        public int Inmueble_propietario { get; set; }
    }
}
