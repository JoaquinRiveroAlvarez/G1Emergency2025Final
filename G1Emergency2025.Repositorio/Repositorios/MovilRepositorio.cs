using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace G1Emergency2025.Repositorio.Repositorios
{
    public class MovilRepositorio : Repositorio<Movil>, IMovilRepositorio
    {
        private readonly AppDbContext context;

        public MovilRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task AsociarEvento(int movilId, int eventoId)
        {
            var rel = new EventoMovil
            {
                MovilId = movilId,
                EventoId = eventoId
            };

            context.EventoMovils.Add(rel);
            await context.SaveChangesAsync();
        }
        public async Task<MovilListadoDTO?> SelectMovilPorId(int id)
        {
            var movil = await context.Movils
                .Include(m => m.TipoMovils)
                .Include(m => m.EventoMovils)
                    .ThenInclude(em => em.Evento)
                .Where(m => m.Id == id)
                .Select(m => new MovilListadoDTO
                {
                    Id = m.Id,
                    disponibilidadMovil = m.disponibilidadMovil,
                    Patente = m.Patente,
                    TipoMovil = m.TipoMovils!.Tipo,
                    CodigoMovil = m.TipoMovils!.Codigo,

                    Eventos = m.EventoMovils
                        .Select(em => new EventoMovilResumenDTO
                        {
                            EventoId = em.Evento!.Id,
                            Codigo = em.Evento.Codigo,
                            colorEvento = em.Evento.colorEvento,
                            FechaHora = em.Evento.FechaHora,
                            Ubicacion = em.Evento.Ubicacion,
                            Relato = em.Evento.Relato,
                            TipoEstado = em.Evento.TipoEstados!.Tipo
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return movil;
        }
        public async Task<List<MovilListadoDTO>> SelectListaMovil()
        {
            var lista = await context.Movils
                .Include(m => m.TipoMovils)
                .Include(m => m.EventoMovils)
                    .ThenInclude(em => em.Evento)
                        .ThenInclude(e => e!.TipoEstados)
                .Select(m => new MovilListadoDTO
                {
                    Id = m.Id,
                    disponibilidadMovil = m.disponibilidadMovil,
                    Patente = m.Patente,
                    TipoMovil = m.TipoMovils!.Tipo,
                    CodigoMovil = m.TipoMovils!.Codigo,

                    Eventos = m.EventoMovils
                        .Select(em => new EventoMovilResumenDTO
                        {
                            EventoId = em.Evento!.Id,
                            Codigo = em.Evento.Codigo,
                            colorEvento = em.Evento.colorEvento,
                            FechaHora = em.Evento.FechaHora,
                            Ubicacion = em.Evento.Ubicacion,
                            Relato = em.Evento.Relato,
                            TipoEstado = em.Evento.TipoEstados!.Tipo
                        })
                        .ToList()
                })
                .ToListAsync();

            return lista;
        }

        public async Task<MovilConEventosDTO> SelectListaMovilPorPatente(string patente)
        {
            var movil = await context.Movils
                .Include(m => m.TipoMovils)
                .Include(m => m.EventoMovils)
                    .ThenInclude(em => em.Evento)
                .Where(m => m.Patente == patente)
                .Select(m => new MovilConEventosDTO
                {
                    Patente = m.Patente,
                    TipoMovil = m.TipoMovils!.Tipo,
                    TipoMovilCod = m.TipoMovils!.Codigo,
                    Eventos = m.EventoMovils.Select(em => new EventoResumenDTO
                    {
                        Id = em.Evento!.Id,
                        Codigo = em.Evento.Codigo,
                        FechaHora = em.Evento.FechaHora,
                        Relato = em.Evento.Relato
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return movil!;
        }

        public async Task<List<MovilConEventosDTO>> SelectListaMovilPorCodTipoMovil(string codigo)
        {
            var moviles = await context.Movils
                .Include(m => m.TipoMovils)
                .Include(m => m.EventoMovils)
                    .ThenInclude(em => em.Evento)
                .Where(m => m.TipoMovils!.Codigo == codigo)
                .Select(m => new MovilConEventosDTO
                {
                    Patente = m.Patente,
                    TipoMovilCod = m.TipoMovils!.Codigo,
                    TipoMovil = m.TipoMovils!.Tipo,
                    Eventos = m.EventoMovils.Select(em => new EventoResumenDTO
                    {
                        Id = em.Evento!.Id,
                        Codigo = em.Evento.Codigo,
                        FechaHora = em.Evento.FechaHora,
                        Relato = em.Evento.Relato
                    }).ToList()
                })
                .ToListAsync();

            return moviles;
        }

        public async Task<List<MovilConEventosDTO>> SelectListaMovilPorTipoMovil(string tipoMovil)
        {
            var moviles = await context.Movils
                .Include(m => m.TipoMovils)
                .Include(m => m.EventoMovils)
                    .ThenInclude(em => em.Evento)
                .Where(m => m.TipoMovils!.Tipo == tipoMovil)
                .Select(m => new MovilConEventosDTO
                {
                    Patente = m.Patente,
                    TipoMovilCod = m.TipoMovils!.Codigo,
                    TipoMovil = m.TipoMovils!.Tipo,
                    Eventos = m.EventoMovils.Select(em => new EventoResumenDTO
                    {
                        Id = em.Evento!.Id,
                        Codigo = em.Evento.Codigo,
                        FechaHora = em.Evento.FechaHora,
                        Relato = em.Evento.Relato
                    }).ToList()
                })
                .ToListAsync();

            return moviles;
        }

        public async Task<List<MovilListadoDTO>> SelectListaMovilPorDisponibilidad(DisponibilidadMovil disponibilidadMovil)
        {
            var lista = await context.Movils
                .Include(m => m.TipoMovils)
                .Where(m => m.disponibilidadMovil == disponibilidadMovil)
                .Select(m => new MovilListadoDTO
                {
                    Id = m.Id,
                    disponibilidadMovil = m.disponibilidadMovil,
                    Patente = m.Patente,
                    TipoMovil = m.TipoMovils!.Tipo
                })
                .OrderBy(m => m.disponibilidadMovil)
                .ToListAsync();
            return lista;
        }
    }
}
