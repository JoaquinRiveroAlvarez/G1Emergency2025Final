using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class EventoUsuario
    {
        public int EventoId { get; set; }
        public Evento? Eventos { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuarios { get; set; }
    }
}
