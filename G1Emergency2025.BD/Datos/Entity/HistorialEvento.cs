using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class HistorialEvento : EntityBase
    {
        public bool CreoEvento { get; set; } = false;
        public bool ModificoEvento { get; set; } = false;
        public int EventoId { get; set; }
        public Evento? Evento { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
