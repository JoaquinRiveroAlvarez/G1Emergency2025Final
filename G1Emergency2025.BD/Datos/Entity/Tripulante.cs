using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class Tripulante : EntityBase
    {

        [Required(ErrorMessage = "Si esta en el movil es obligatorio")]
        public required bool EnMovil { get; set; } 
        public int PersonaId { get; set; }
        public Persona? Personas { get; set; }

    }
}
