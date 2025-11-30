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
    public class TipoMovilRepositorio : Repositorio<TipoMovil>, ITipoMovilRepositorio
    {
        private readonly AppDbContext context;

        public TipoMovilRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<TipoMovil?> SelectByCod(string cod)
        {
            return await context.Set<TipoMovil>().FirstOrDefaultAsync(x => x.Codigo == cod);
        }
        public async Task<List<TipoMovilListadoDTO>> SelectListaTipoMovil()
        {
            var lista = await context.TipoMovils
                                    .Select(p => new TipoMovilListadoDTO
                                    {
                                        Id = p.Id,
                                        TipoMovil = $" Codigo: {p.Codigo} - Tipo: {p.Tipo}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
