using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteEventoResumenDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public List<PacienteResumenDTO> Pacientes { get; set; } = new();
    }
}
