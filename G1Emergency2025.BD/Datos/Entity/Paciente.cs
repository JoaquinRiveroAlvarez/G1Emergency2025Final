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
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public string ObraSocial { get; set; } = "Ninguna";
        public string HistoriaClinica { get; set; } = "Ninguna";
        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }
        public List<PacienteEvento> PacienteEventos { get; set; } = new ();
    }
}
