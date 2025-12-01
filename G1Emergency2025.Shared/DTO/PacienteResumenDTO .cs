using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteResumenDTO 
    {
        public int Id { get; set; }
        public string ObraSocial { get; set; } = "Ninguna";
        public string NombrePersona {  get; set; } = string.Empty;
        public string DNIPersona {  get; set; } = string.Empty;
        public string DireccionPersona {  get; set; } = string.Empty;
        public Sexo SexoPersona {  get; set; }
        public string EdadPersona {  get; set; } = string.Empty;
        public string HistoriaClinica {  get; set; } = string.Empty;
        public List<EventoPacienteMostrarDTO> Eventos { get; set; } = new();
    }
}
