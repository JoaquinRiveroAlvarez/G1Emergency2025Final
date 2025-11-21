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
