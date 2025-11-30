using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TripulacionActualListadoDTO
    {
        public int Id { get; set; }
        public DateTime FechaEntrada { get; set; } = DateTime.Now;
        public string TripulanteNombre { get; set; } = string.Empty;
        public string TripulanteLegajo { get; set; } = string.Empty;
        public string TipoDelTripulante { get; set; } = string.Empty;
        public string TipoDelMovil { get; set; } = string.Empty;
        public string MovilPatente { get; set; } = string.Empty;
    }
}
