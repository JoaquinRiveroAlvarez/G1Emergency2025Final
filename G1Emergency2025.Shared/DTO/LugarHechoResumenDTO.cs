using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class LugarHechoResumenDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
