using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class Paciente : EntityBase
    {

        [Required(ErrorMessage = "La obra social es obligatoria")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public required string ObraSocial { get; set; }
        public required string NombrePersona { get; set; } 
        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }
        public List<PacienteEvento> PacienteEventos { get; set; } = new ();
  
    }
}
