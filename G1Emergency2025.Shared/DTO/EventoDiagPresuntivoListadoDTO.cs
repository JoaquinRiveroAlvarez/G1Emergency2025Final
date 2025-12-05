using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoDiagPresuntivoListadoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Relato { get; set; } = string.Empty;
        public ColorEvento colorEvento { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string Causa { get; set; } = string.Empty;
        public string TipoEstado { get; set; } = string.Empty;
        public int TipoEstadoId { get; set; }
        public List<PacienteDiagPresuntivoDTO> Pacientes { get; set; } = new();
        public List<UsuarioResumenDTO> Usuarios { get; set; } = new();
        public List<LugarHechoResumenDTO> Lugares { get; set; } = new();
        public List<MovilResumenDTO> Moviles { get; set; } = new();
        public List<HistorialEventoDTO> Historial { get; set; } = new();
    }
}
