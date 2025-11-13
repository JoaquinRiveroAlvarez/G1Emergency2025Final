using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public class HistorialEventoRepositorio : Repositorio<HistorialEvento>, IHistorialEventoRepositorio
    {
        private readonly AppDbContext context;
        public HistorialEventoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<HistorialEvento?> SelectById(int id)
        {
            HistorialEvento? entidad = await context.HistorialEventos.FirstOrDefaultAsync(x => x.Id == id);
            return entidad;
        }
        public async Task<List<HistorialEventoListadoDTO?>> SelectListaHistorialEvento()
        {
            var lista = await context.HistorialEventos
                .Include(h => h.Evento)
                .Include(h => h.Usuario)
                                    .Select(p => new HistorialEventoListadoDTO
                                    {
                                        Id = p.Id,
                                        Evento = p.EventoId,
                                        EventoCodigo = p.Evento!.Codigo,
                                        Usuario = p.Usuario!.Id,
                                        UsuarioNombre = p.Usuario.Nombre,
                                        CreoEvento = p.CreoEvento,
                                        ModificoEvento = p.ModificoEvento
                                    })
                                    .ToListAsync();
            return lista!;
        }

        public async Task<HistorialEventoListadoDTO?> SelectPorId(int id)
        {
            return await context.HistorialEventos
                .Include(h => h.Evento)
                .Include(h => h.Usuario)
                .Where(h => h.Id == id)
                .Select(p => new HistorialEventoListadoDTO
                {
                    Id = p.Id,
                    Evento = p.EventoId,
                    EventoCodigo = p.Evento!.Codigo,
                    Usuario = p.Usuario!.Id,
                    UsuarioNombre = p.Usuario.Nombre,
                    CreoEvento = p.CreoEvento,
                    ModificoEvento = p.ModificoEvento
                })
                .FirstOrDefaultAsync();
        }   

    }
}
