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
                                        TripulacionActual = $" Fecha de Entrada: {p.FechaEntrada} - Id Tripulante: {p.TripulanteId} - Nombre y Apellido:{p.Tripulantes!.Personas!.Nombre} - Id Movil: {p.MovilId} - Patente Movil: {p.Movils!.Patente}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
