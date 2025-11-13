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
    public class TipoEstadoRepositorio : Repositorio<TipoEstado>, ITipoEstadoRepositorio
    {
        private readonly AppDbContext context;

        public TipoEstadoRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<TipoEstado?> SelectByCod(string cod)
        {
            return await context.Set<TipoEstado>().FirstOrDefaultAsync(x => x.Codigo == cod);
        }
        public async Task<List<TipoEstadoListadoDTO>> SelectListaTipoEstado()
        {
            var lista = await context.TipoEstados
                                    .Select(p => new TipoEstadoListadoDTO
                                    {
                                        Id = p.Id,
                                        TipoEstado = $"Código: {p.Codigo} - Tipo: {p.Tipo}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
