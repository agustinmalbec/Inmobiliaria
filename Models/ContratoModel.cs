using System.ComponentModel.DataAnnotations;

namespace Inmobiliaria.Models
{
    public class Contrato
    {
        public int Id { get; set; }

        [Required]
        public int Contrato_inquilino { get; set; }

        [Required]
        public int Contrato_inmueble { get; set; }

        [Required]
        public DateTime Fecha_desde { get; set; }

        [Required]
        public DateTime Fecha_hasta { get; set; }

        [Required]
        public int Monto { get; set; }
    }
}
