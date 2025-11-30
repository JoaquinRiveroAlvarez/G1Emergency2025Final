using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoListadoDTO 
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public required ColorEvento colorEvento { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public string Causa { get; set; } = string.Empty;
        public string TipoEstado { get; set; } = string.Empty;
        //public int CausaId { get; set; }
        public int TipoEstadoId { get; set; }
        public List<PacienteResumenDTO> Pacientes { get; set; } = new();
        public List<UsuarioResumenDTO> Usuarios { get; set; } = new();
        public List<LugarHechoResumenDTO> Lugares { get; set; } = new();
        public List<MovilResumenDTO> Moviles { get; set; } = new();
        //public List<int> PacienteIds { get; set; } = new();
        //public List<int> UsuarioIds { get; set; } = new();
        //public List<int> LugarHechoIds { get; set; } = new();
    }
}
