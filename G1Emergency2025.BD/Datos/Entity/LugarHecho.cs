using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    [Index(nameof(Codigo), Name = "LugarHecho_UQ", IsUnique = true)]

    public class LugarHecho : EntityBase
    {

        [Required(ErrorMessage = "El código es obligatorio")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public required string Codigo { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public required string Tipo { get; set; }

        [Required(ErrorMessage = "La descripcion del lugar del hecho es obligatoria")]
        [MaxLength(100, ErrorMessage = "La cantidad Maxima de caracteres es 100")]
        public required string Descripcion { get; set; }

        public List<EventoLugarHecho> EventoLugarHechos { get; set; } = new();

    }
}
