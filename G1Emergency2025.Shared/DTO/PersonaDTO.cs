using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class PersonaDTO
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "El DNI no puede exceder los 50 caracteres.")]
        public string DNI { get; set; } = string.Empty;

        [MaxLength(50, ErrorMessage = "El Legajo no puede exceder los 50 caracteres.")]
        public string Legajo { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres.")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Sexo es obligatorio.")]
        public Sexo Sexo { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La edad no puede exceder los 3 caracteres.")]
        public string Edad { get; set; } = string.Empty;
    }
}
