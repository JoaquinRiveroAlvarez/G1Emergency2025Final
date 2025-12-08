using G1Emergency2025.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Shared.DTO
{
    public class AsignarMovilesEventoDTO
    {
        public int Id { get; set; }
        public List<int> MovilIds { get; set; } = new();
    }
}
