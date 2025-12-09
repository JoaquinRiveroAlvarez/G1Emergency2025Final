using G1Emergency2025.Shared.Enum;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class PacienteEvento
    {
        public EnumEstadoRegistro EstadoRegistro { get; set; } = EnumEstadoRegistro.activo;
        public int EventoId { get; set; }
        public Evento? Eventos { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Pacientes { get; set; }

        [MaxLength(100, ErrorMessage = "La cantidad máxima de caracteres es 100")]
        public string DiagnosticoPresuntivo { get; set; } = "Sin Diagnostico";
    }
}
