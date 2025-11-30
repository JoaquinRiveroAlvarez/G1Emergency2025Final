using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class HistorialEventoListadoDTO
    {
        public int Id { get; set; }
        public bool CreoEvento { get; set; } = false;
        public bool ModificoEvento { get; set; } = false;
        public int Evento { get; set; }
        public string EventoCodigo { get; set; } = string.Empty;
        public int Usuario { get; set; }
        public string UsuarioNombre { get; set; } = string.Empty;
    }
}
