using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TripulanteListadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public string Legajo { get; set; } = string.Empty;
        public string Edad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public Sexo Sexo { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public bool EnMovil { get; set; } = false;

    }
}
