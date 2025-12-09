using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PacienteDTO 
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public string ObraSocial { get; set; } = "Ninguna";
        public string HistoriaClinica { get; set; } = "Ninguna";
        public PersonaDTO PersonaDTO { get; set; } = null!;
        public int PersonaId { get; set; }
        public string NombrePersona { get; set; } = string.Empty;

    }
}
