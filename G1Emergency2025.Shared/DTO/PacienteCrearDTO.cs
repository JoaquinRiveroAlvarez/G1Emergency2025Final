using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteCrearDTO
    {
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public string ObraSocial { get; set; } = "Ninguna";
        public string HistoriaClinica { get; set; } = "";
        public PersonaCrearDTO Persona { get; set; } = new PersonaCrearDTO();
        public List<EventoPacienteDTO> Eventos { get; set; } = new ();
        public string DiagnosticoPresuntivo { get; set; } = "Sin Diagnosticos";
    }
}
