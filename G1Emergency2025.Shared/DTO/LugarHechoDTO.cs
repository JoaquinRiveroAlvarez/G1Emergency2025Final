using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class LugarHechoDTO
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La descripcion del lugar del hecho es obligatoria")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public string Descripcion { get; set; }
    }
}
