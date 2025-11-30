using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class RolDTO
    {
        [Required(ErrorMessage = "El Código es obligatorio.")]
        [MaxLength(3, ErrorMessage = "El Código no puede exceder los 3 caracteres.")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Tipo no puede exceder los 50 caracteres.")]
        public required string Tipo { get; set; }
    }
}
