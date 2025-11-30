using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoPacienteMostrarDTO
    {
        public int EventoId { get; set; }
        public string CodigoEvento { get; set; } = string.Empty;
        public string DiagnosticoPresuntivo { get; set; } = "Sin Diagnostico";
    }

}
