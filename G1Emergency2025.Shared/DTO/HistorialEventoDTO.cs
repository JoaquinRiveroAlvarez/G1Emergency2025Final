using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class HistorialEventoDTO
    {
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
        public bool CreoEvento { get; set; }
        public bool ModificoEvento { get; set; }
        public int CantidadModificaciones { get; set; }
    }

}
