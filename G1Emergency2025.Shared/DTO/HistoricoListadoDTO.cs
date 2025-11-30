using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class HistoricoListadoDTO
    {
        public int Id { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string TipoMovil { get; set; } = string.Empty;
        public string PatenteMovil { get; set; } = string.Empty;
        public string TipoTripulante { get; set; } = string.Empty;
        public string NombreTripulante { get; set; } = string.Empty;
        public string LegajoTripulante { get; set; } = string.Empty;
        public string EstadoServicio => FechaSalida == null ? "En servicio" : "Finalizado";
    }
}
