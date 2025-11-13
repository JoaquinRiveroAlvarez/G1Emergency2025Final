using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace G1Emergency2025.Repositorio.Repositorios
{
    public class LugarHechoRepositorio : Repositorio<LugarHecho>, ILugarHechoRepositorio
    {
        private readonly AppDbContext context;

        public LugarHechoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<LugarHecho?> SelectByCod(string cod)
        {
            return await context.Set<LugarHecho>().FirstOrDefaultAsync(x => x.Codigo == cod);
        }
        public async Task<List<LugarHechoListadoDTO>> SelectListaLugarHecho()
        {
            var lista = await context.LugarHechos
                                    .Select(p => new LugarHechoListadoDTO
                                    {
                                        Id = p.Id,
                                        LugarHecho = $"Código: {p.Codigo} - Tipo: {p.Tipo} -  Descripción: {p.Descripcion}"
                                    })
                                    .ToListAsync();
            return lista;
        }
        public async Task AsociarEvento(int lugarHechoId, int eventoId)
        {
            var rel = new EventoLugarHecho
            {
                LugarHechoId = lugarHechoId,
                EventoId = eventoId
            };

            context.EventoLugarHechos.Add(rel);
            await context.SaveChangesAsync();
        }
    }
}
