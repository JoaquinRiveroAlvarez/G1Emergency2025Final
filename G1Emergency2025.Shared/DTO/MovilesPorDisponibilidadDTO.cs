using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class MovilesPorDisponibilidadDTO
    {
        public DisponibilidadMovil Disponibilidad { get; set; } 
        public List<MovilConEventosDTO> Moviles { get; set; } = new();
    }
}
