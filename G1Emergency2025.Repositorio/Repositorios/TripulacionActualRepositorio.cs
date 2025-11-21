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
    public class TripulacionActualRepositorio : Repositorio<TripulacionActual>, ITripulacionActualRepositorio
    {
        private readonly AppDbContext context;

        public TripulacionActualRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<TripulacionActual?> SelectByFechaEntrada(DateTime FechaEntrada)
        {
            return await context.Set<TripulacionActual>().FirstOrDefaultAsync(x => x.FechaEntrada == FechaEntrada);
        }
        public async Task<List<TripulacionActualListadoDTO>> SelectListaTripulacionActual()
        {
            var lista = await context.TripulacionActuals
            .Select(p => new TripulacionActualListadoDTO
            {
                Id = p.Id,
                FechaEntrada = p.FechaEntrada 
            })
            .ToListAsync();
            return lista;
        }
        public async Task<List<TripulanteEnMovilDTO>> SelectTripulanteEnMoviles()
        {
            var lista = await context.TripulacionActuals
                .Include(t => t.Movils)
                    .ThenInclude(m => m!.TipoMovils)
                .Include(t => t.Tripulantes)
                    .ThenInclude(tr => tr!.Personas)
                .Include(t => t.Tripulantes)
                    .ThenInclude(tt => tt!.TipoTripulantes)

                .Select(t => new TripulanteEnMovilDTO
                {
                    Nombre = t.Tripulantes!.Personas!.Nombre,
                    DNI = t.Tripulantes.Personas.DNI,
                    Legajo = t.Tripulantes.Personas.Legajo,
                    TipoTripulante = t.Tripulantes!.TipoTripulantes!.Tipo,
                    TipoMovil = t.Movils!.TipoMovils!.Tipo,
                    PatenteMovil = t.Movils.Patente,
                    EnMovil = t.Tripulantes.EnMovil,
                    FechaEntrada = t.FechaEntrada
                })
                .ToListAsync();
            return lista;
        }
    }
}
