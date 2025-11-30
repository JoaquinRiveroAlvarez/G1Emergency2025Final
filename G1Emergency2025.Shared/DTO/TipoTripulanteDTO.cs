using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TipoTripulanteDTO 
    {
        [Required(ErrorMessage = "El Código es obligatorio.")]
        [MaxLength(2, ErrorMessage = "La cantidad Maxima de caracteres es 2")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [MaxLength(50, ErrorMessage = "La cantidad Maxima de caracteres es 50")]
        public required string Tipo { get; set; }
    }
}
