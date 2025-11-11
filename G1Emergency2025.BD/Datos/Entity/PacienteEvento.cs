using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos.Entity
{
    public class PacienteEvento
    {
        public int EventoId { get; set; }
        public Evento? Eventos { get; set; }
        public int PacienteId { get; set; }
        public Paciente? Pacientes { get; set; }
    }
}
