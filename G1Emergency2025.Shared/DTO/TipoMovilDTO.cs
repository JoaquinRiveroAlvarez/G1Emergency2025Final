using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TipoMovilDTO 
    {

        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(2, ErrorMessage = "La cantidad Maxima de caracteres es 2")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public required string Tipo { get; set; }
    }
}
