using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    [Index(nameof(HistoriaClinica), Name = "Paciente_UQ", IsUnique = true)]
    public class Paciente : EntityBase
    {
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public string ObraSocial { get; set; } = "Ninguna";

        //[Required(ErrorMessage = "El código es obligatorio")]
        public string HistoriaClinica { get; set; }
        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }
        public List<PacienteEvento> PacienteEventos { get; set; } = new ();
    }
}
