using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteActualizarDTO
    {
        public string? ObraSocial { get; set; }
        public PersonaDTO PersonaDTO { get; set; } = null!;
        public List<int> EventosIds { get; set; } = new List<int>(); // IDs de los eventos seleccionados
    }
}
