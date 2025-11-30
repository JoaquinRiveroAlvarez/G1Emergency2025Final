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
    public class HistoricoRepositorio : Repositorio<Historico>, IHistoricoRepositorio
    {
        private readonly AppDbContext context;

        public HistoricoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Historico?> SelectByFechaEntrada(DateTime FechaEntrada)
        {
            return await context.Set<Historico>().FirstOrDefaultAsync(x => x.FechaEntrada == FechaEntrada);
        }
        public async Task<Historico?> SelectByFechaSalida(DateTime FechaSalida)
        {
            return await context.Set<Historico>().FirstOrDefaultAsync(x => x.FechaSalida == FechaSalida);
        }
        public async Task<List<HistoricoListadoDTO>> SelectListaHistorico()
        {
            var lista = await context.Historicos
            .Include(p => p.Movil)
                  .ThenInclude(m => m!.TipoMovils)
            .Include(p => p.Tripulantes)
                  .ThenInclude(t => t!.Personas)
            .Select(p => new HistoricoListadoDTO
            {
                Id = p.Id,
                FechaEntrada = p.FechaEntrada,
                FechaSalida = p.FechaSalida,
                TipoMovil = p.Movil!.TipoMovils!.Tipo,
                PatenteMovil = p.Movil!.Patente,
                TipoTripulante = p.Tripulantes!.TipoTripulantes!.Tipo,
                NombreTripulante = p.Tripulantes!.Personas!.Nombre,
                LegajoTripulante = p.Tripulantes.Personas.Legajo
            })
            .ToListAsync();
            return lista;
        }
        public async Task RegistrarEntrada(int tripulanteId, int movilId)
        {
            var registro = new Historico
            {
                TripulanteId = tripulanteId,
                MovilId = movilId,
                FechaEntrada = DateTime.Now,
                FechaSalida = null 
            };

            await context.Historicos.AddAsync(registro);
            await context.SaveChangesAsync();
        }

        public async Task RegistrarSalida(int tripulanteId, int movilId)
        {
            var registro = await context.Historicos
                .Where(h => h.TripulanteId == tripulanteId
                         && h.MovilId == movilId
                         && h.FechaSalida == null)
                .OrderByDescending(h => h.FechaEntrada)
                .FirstOrDefaultAsync();

            if (registro == null)
                throw new Exception($"No se encontró un registro activo en Historico para este tripulanteId{tripulanteId} y móvilId{movilId}.");

            registro.FechaSalida = DateTime.Now;
            await context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id, Historico entidad)
        {
            var entity = await context.Set<Historico>().FindAsync(id);
            if (entity == null)
                return false;

            context.Set<Historico>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
