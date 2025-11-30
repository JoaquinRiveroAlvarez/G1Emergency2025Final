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
    public class CausaRepositorio : Repositorio<Causa>, ICausaRepositorio
    {
        private readonly AppDbContext context;

        public CausaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Causa?> SelectByCod(string cod)
        {
            Causa? entidad = await context.Causas.FirstOrDefaultAsync(x => x.Codigo == cod);
            return entidad;
        }
        public async Task<List<CausaListadoDTO>> SelectListaCausa()
        {
            var lista = await context.Causas
                                    .Select(p => new CausaListadoDTO
                                    {
                                        Id = p.Id,
                                        Codigo = $" Codigo: {p.Codigo} - Posible Causa: {p.posibleCausa}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
