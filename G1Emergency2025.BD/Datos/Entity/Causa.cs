using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    [Index(nameof(Codigo), Name = "Causa_UQ", IsUnique = true)]
    public class Causa : EntityBase
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(5, ErrorMessage = "Maximo 5 caracteres")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "La causa es obligatoria")]
        [MaxLength(200, ErrorMessage = "Ingrese una causa válida")]
        public required string posibleCausa { get; set; }

    }
}
