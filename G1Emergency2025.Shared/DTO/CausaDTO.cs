using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class CausaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(5, ErrorMessage = "Maximo 5 caracteres")]
        public string Codigo { get; set; } = "";

        [Required(ErrorMessage = "La causa es obligatoria")]
        [MaxLength(200, ErrorMessage = "Ingrese una causa válida")]
        public string posibleCausa { get; set; } = "";
    }
}
