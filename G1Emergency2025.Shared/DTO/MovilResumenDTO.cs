using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class MovilResumenDTO
    {
        public int Id { get; set; }
        public string Patente { get; set; } = string.Empty;
        public string TipoMovil { get; set; } = string.Empty;
        public DisponibilidadMovil disponibilidadMovil { get; set; }
    }
}
