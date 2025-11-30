using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TripulacionActualDTO 
    {

        [Required(ErrorMessage = "La Fecha de entrada es obligatoria")]
        [MaxLength(50, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public required DateTime FechaEntrada { get; set; } = DateTime.Now;
        public int TripulanteId { get; set; }
        public int MovilId { get; set; }
    }
}
