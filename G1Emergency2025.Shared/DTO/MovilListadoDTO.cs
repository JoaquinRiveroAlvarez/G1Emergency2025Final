using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class MovilListadoDTO 
    {
        public int Id { get; set; }
        public DisponibilidadMovil disponibilidadMovil { get; set; } = DisponibilidadMovil.libre;
        public string Patente { get; set; } = string.Empty;
        public string TipoMovil { get; set; } = string.Empty;
        public string CodigoMovil { get; set; } = string.Empty; 
        public List<EventoMovilResumenDTO> Eventos { get; set; } = new();

    }
}
