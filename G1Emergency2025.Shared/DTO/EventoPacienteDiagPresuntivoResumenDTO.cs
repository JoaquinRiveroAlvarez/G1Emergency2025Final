using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class EventoPacienteDiagPresuntivoResumenDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public List<PacienteDiagPresuntivoDTO> Pacientes { get; set; } = new();
    }
}
