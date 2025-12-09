using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteActualizarDTO
    {
        public int Id { get; set; }
        public string HistoriaClinica { get; set; } = "";
        public string ObraSocial { get; set; } = "Ninguna";
        public PersonaDTO PersonaDTO { get; set; } = null!; 
        public List<EventoPacienteDTO> Eventos { get; set; } = new();
    }
}
