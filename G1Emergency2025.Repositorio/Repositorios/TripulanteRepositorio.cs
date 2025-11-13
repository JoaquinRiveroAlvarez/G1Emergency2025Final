using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G1Emergency2025.Shared.DTO;
namespace G1Emergency2025.Repositorio.Repositorios
 
{
    public class TripulanteRepositorio : Repositorio<Tripulante>, ITripulanteRepositorio
    {
        private readonly AppDbContext context;

        public TripulanteRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Tripulante?> SelectByEnMovil(bool EnMovil)
        {
            return await context.Set<Tripulante>().FirstOrDefaultAsync(x => x.EnMovil == EnMovil);
        }
        public async Task<List<TripulanteListadoDTO>> SelectListaTripulante()
        {
            var lista = await context.Tripulantes
                                    .Select(p => new TripulanteListadoDTO
                                    {
                                        Id = p.Id,
                                        Tripulante = $" En Movil: {p.EnMovil} - Id Persona: {p.PersonaId} - Nombre y Apellido: {p.Personas!.Nombre} - DNI: {p.Personas!.DNI}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
