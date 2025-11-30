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
        private readonly ITripulanteRepositorio tripulanteRepositorio;
        private readonly IHistoricoRepositorio historicoRepositorio;

        public TripulacionActualRepositorio(
            ITripulanteRepositorio tripulanteRepositorio,
            IHistoricoRepositorio historicoRepositorio,
            AppDbContext context) : base(context)
        {
            this.context = context;
            this.tripulanteRepositorio = tripulanteRepositorio;
            this.historicoRepositorio = historicoRepositorio;
        }
        public async Task<TripulacionActual?> SelectByFechaEntrada(DateTime FechaEntrada)
        {
            return await context.Set<TripulacionActual>().FirstOrDefaultAsync(x => x.FechaEntrada == FechaEntrada);
        }
        public async Task<List<TripulacionActualListadoDTO>> SelectListaTripulacionActual()
        {
            var lista = await context.TripulacionActuals
                .Include(t => t.Tripulantes)
                      .ThenInclude(tr => tr!.Personas)
                .Include(t => t.Tripulantes)
                      .ThenInclude(tt => tt!.TipoTripulantes)
                .Include(t => t.Movils)
                      .ThenInclude(m => m!.TipoMovils)
            .Select(p => new TripulacionActualListadoDTO
            {
                Id = p.Id,
                FechaEntrada = p.FechaEntrada,
                TipoDelTripulante = p.Tripulantes!.TipoTripulantes!.Tipo,
                TripulanteNombre = p.Tripulantes!.Personas!.Nombre,
                TripulanteLegajo = p.Tripulantes.Personas.Legajo,
                TipoDelMovil = p.Movils!.TipoMovils!.Tipo,
                MovilPatente = p.Movils!.Patente
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
                    Id = t.Id,
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
        public async Task<List<TripulanteActualEnEventoDTO>> ObtenerTripulantesPorEvento(string eventoCod)
        {
            var evento = await context.Eventos
                .FirstOrDefaultAsync(e => e.Codigo == eventoCod);
            if (evento == null)
                throw new Exception($"No se encontró un evento con código {eventoCod}");

            var eventoId = evento.Id;

            var tripulantes = await context.TripulacionActuals
                .Include(t => t.Tripulantes)
                    .ThenInclude(tr => tr!.Personas)
                .Include(t => t.Tripulantes)
                    .ThenInclude(tr => tr!.TipoTripulantes)
                .Include(t => t.Movils)
                    .ThenInclude(m => m!.EventoMovils
                        .Where(me => me.EventoId == eventoId))
                .Where(t => t.Movils!.EventoMovils.Any(me => me.EventoId == eventoId))
                .Select(t => new TripulanteActualEnEventoDTO
                {
                    Nombre = t.Tripulantes!.Personas!.Nombre,
                    Legajo = t.Tripulantes.Personas.Legajo,

                    CodTipoTripulante = t.Tripulantes!.TipoTripulantes!.Codigo,
                    TipoTripulante = t.Tripulantes.TipoTripulantes.Tipo,

                    TipoMovil = t.Movils!.TipoMovils!.Tipo,
                    PatenteMovil = t.Movils.Patente,
                    FechaEntrada = t.FechaEntrada
                })
                .ToListAsync();

            return tripulantes;
        }

        public async Task AsignarTripulante(int tripulanteId, int movilId)
        {
            var tripulante = await context.Tripulantes
                .FirstOrDefaultAsync(t => t.Id == tripulanteId);

            if (tripulante == null)
                throw new Exception($"Tripulante con ID {tripulanteId} no encontrado.");

            var asignacion = new TripulacionActual
            {
                TripulanteId = tripulante.Id,
                MovilId = movilId,
                FechaEntrada = DateTime.Now
            };

            await context.TripulacionActuals.AddAsync(asignacion);
            await context.SaveChangesAsync();
        }
        public async Task FinalizarEvento(int eventoId)
        {
            var movilIds = await context.EventoMovils
                .Where(me => me.EventoId == eventoId)
                .Select(me => me.MovilId)
                .ToListAsync();

            var asignaciones = await context.TripulacionActuals
                .Where(t => movilIds.Contains(t.MovilId))
                .ToListAsync();

            foreach (var asignacion in asignaciones)
            {
                Console.WriteLine($"TripulanteId={asignacion.TripulanteId}, MovilId={asignacion.MovilId}");
                await historicoRepositorio.RegistrarSalida(asignacion.TripulanteId, asignacion.MovilId);
                await tripulanteRepositorio.ActualizarEstadoEnMovil(asignacion.TripulanteId, false);
                context.TripulacionActuals.Remove(asignacion);
            }

            await context.SaveChangesAsync();
        }
    }
}
