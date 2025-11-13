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
    public class RolRepositorio : Repositorio<Rol>, IRolRepositorio
    {
        private readonly AppDbContext context;

        public RolRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Rol?> SelectByCod(string cod)
        {
            return await context.Set<Rol>().FirstOrDefaultAsync(x => x.Codigo == cod);
        }
        public async Task<List<RolListadoDTO>> SelectListaRol()
        {
            var lista = await context.Rols
                                    .Select(p => new RolListadoDTO
                                    {
                                        Id = p.Id,
                                        Rol = $" Código: {p.Codigo} - Tipo: {p.Tipo}"
                                    })
                                    .ToListAsync();
            return lista;
        }
    }
}
