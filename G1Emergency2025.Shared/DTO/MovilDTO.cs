using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class MovilDTO
    {
        [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
        public DisponibilidadMovil disponibilidadMovil { get; set; } = DisponibilidadMovil.libre;

        [Required(ErrorMessage = "La patente es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La patente no puede exceder los 50 caracteres.")]
        public required string Patente { get; set; }
        public int EventoId { get; set; }
    }
}
