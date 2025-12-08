using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoMovilResumenDTO
    {
        public int EventoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public ColorEvento colorEvento { get; set; }
        public DateTime FechaHora { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Relato { get; set; } = string.Empty;
        public string TipoEstado { get; set; } = string.Empty;
    }

}
