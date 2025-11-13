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
    public class MovilRepositorio : Repositorio<Movil>, IMovilRepositorio
    {
        private readonly AppDbContext context;

        public MovilRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Movil?> SelectByCod(string cod)
        {
            return await context.Set<Movil>().FirstOrDefaultAsync(x => x.Patente == cod);
        }
        public async Task<List<MovilListadoDTO>> SelectListaMovil()
        {
            var lista = await context.Movils
                                    .Select(p => new MovilListadoDTO
                                    {
                                        Id = p.Id,
                                        Movil = $" Disponibilidad de Movil: {p.disponibilidadMovil} - Patente: {p.Patente} - Codigo Evento: {p.Evento!.Codigo}"
                                    })
                                    .ToListAsync();
            return lista;
        }

    }
}
