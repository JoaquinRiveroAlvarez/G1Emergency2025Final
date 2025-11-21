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
        public int TripulanteId { get; set; }
        public int MovilId { get; set; }
    }
}
