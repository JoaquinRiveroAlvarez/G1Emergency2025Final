using G1Emergency2025.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    [Index(nameof(Patente), Name = "Patente_UQ", IsUnique = true)]
    public class Movil : EntityBase
    {

        [Required(ErrorMessage = "La disponibilidad es obligatoria.")]
        public DisponibilidadMovil disponibilidadMovil { get; set; } = DisponibilidadMovil.libre;

        [Required(ErrorMessage = "La patente es obligatoria.")]
        [MaxLength(50, ErrorMessage = "La patente no puede exceder los 50 caracteres.")]
        public required string Patente { get; set; }
        public int TipoMovilId { get; set; }
        public TipoMovil? TipoMovils { get; set; }
        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}
