using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TipoEstadoListadoDTO
    {
        public int Id { get; set; }
        public required string TipoEstado { get; set; }
    }
}
