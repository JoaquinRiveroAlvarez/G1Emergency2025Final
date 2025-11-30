using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class EventoMovil
    {
        public int EventoId { get; set; }
        public Evento? Evento { get; set; }

        public int MovilId { get; set; }
        public Movil? Movil { get; set; }
    }
}
