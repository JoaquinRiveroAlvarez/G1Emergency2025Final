using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class Usuario : EntityBase
    {

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El Nombre no puede exceder los 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El Código es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El Código no puede exceder los 4 caracteres.")]
        public required string Contrasena { get; set; }
        public int PersonaId { get; set; }
        public Persona? Persona{ get; set; }
        public List<EventoUsuario> EventoUsuarios { get; set; } = new();
        public List<UsuarioRol> UsuarioRols { get; set; } = new();
    }
}
