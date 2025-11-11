using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class Historico : EntityBase
    {

        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        public required DateTime FechaEntrada { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha de salida de es obligatoria.")]
        public required DateTime FechaSalida { get; set; }

        public int MovilId { get; set; }
        public Movil? Movil { get; set; }
        public int TripulanteId { get; set; }
        public Tripulante? Tripulantes { get; set; }
    }
}
