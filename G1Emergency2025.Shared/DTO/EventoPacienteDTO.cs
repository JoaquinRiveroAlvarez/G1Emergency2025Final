using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoPacienteDTO
    {
        public int EventoId { get; set; }
        public string DiagnosticoPresuntivo { get; set; } = "Sin Diagnostico";
    }
}
