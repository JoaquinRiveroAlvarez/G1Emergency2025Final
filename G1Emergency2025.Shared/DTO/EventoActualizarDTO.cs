using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoActualizarDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Relato { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un color válido")]
        public ColorEvento colorEvento { get; set; }

        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public string Ubicacion { get; set; } = string.Empty;

        [MaxLength(30, ErrorMessage = "La cantidad Maxima de caracteres es 30")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Fecha y Hora es obligatoria")]
        public DateTime FechaHora { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una causa válida")]
        public int CausaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de estado válido")]
        public int TipoEstadoId { get; set; }
        public List<PacienteCrearDTO> Pacientes { get; set; } = new();
        public List<int> PacienteIds { get; set; } = new();
        public List<int> UsuarioIds { get; set; } = new();
        public List<int> LugarHechoIds { get; set; } = new();
        public List<int> MovilIds { get; set; } = new();
    }
}
