using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class Persona : EntityBase
    {

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Nombre no puede exceder los 100 caracteres.")]
        public required string Nombre { get; set; }

        [MaxLength(50, ErrorMessage = "El DNI no puede exceder los 50 caracteres.")]
        public required string DNI { get; set; }

        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres.")]
        public string Dirección { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Sexo es obligatorio.")]
        public required string Sexo { get; set; }

        [Required(ErrorMessage = "La edad es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La edad no puede exceder los 3 caracteres.")]
        public required string Edad { get; set; }
    }
}
