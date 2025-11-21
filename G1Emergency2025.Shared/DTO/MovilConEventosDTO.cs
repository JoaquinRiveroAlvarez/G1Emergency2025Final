using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class MovilConEventosDTO
    {
        public string Patente { get; set; } = string.Empty;
        public string TipoMovil { get; set; } = string.Empty;
        public string TipoMovilCod { get; set; } = string.Empty;
        public List<EventoResumenDTO> Eventos { get; set; } = new();
    }
}
