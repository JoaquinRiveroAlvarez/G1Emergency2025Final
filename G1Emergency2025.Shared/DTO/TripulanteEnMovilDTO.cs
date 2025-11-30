using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class TripulanteEnMovilDTO
    {
          public int Id { get; set; }
          public string Nombre { get; set; } = string.Empty;
          public string DNI { get; set; } = string.Empty;
          public string Legajo { get; set; } = string.Empty;
          public string TipoTripulante { get; set; } = string.Empty;
          public string TipoMovil { get; set; } = string.Empty;
          public string PatenteMovil { get; set; } = string.Empty;
          public bool EnMovil { get; set; } = true;
          public DateTime FechaEntrada { get; set; } = DateTime.Now;
    }
}
