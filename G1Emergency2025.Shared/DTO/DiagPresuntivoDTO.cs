using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class DiagPresuntivoDTO
    {
        [Required(ErrorMessage = "El Diagnostico presuntivo es obligatorio.")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public required string PosDiagnostico { get; set; }

        [Required(ErrorMessage = "El Id del Paciente es obligatorio.")]
        public int PacienteId { get; set; }

    }
}
