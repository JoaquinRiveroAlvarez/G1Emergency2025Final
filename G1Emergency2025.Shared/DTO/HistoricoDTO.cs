using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class HistoricoDTO 
    {
        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        public required string FechaEntrada { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de salida de es obligatoria.")]
        public required string FechaSalida { get; set; } = string.Empty;

        [Required(ErrorMessage = "El id del Movil es obligatorio.")]
        public required int MovilId { get; set; }

        [Required(ErrorMessage = "El id del Tripulante es obligatorio.")]
        public required int TripulanteId { get; set; }
    }
}
