using G1Emergency2025.BD.Datos;
using G1Emergency2025.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.Repositorio.Repositorios
{
    public class Repositorio<E> : IRepositorio<E> where E : class, IEntityBase
    {
        private readonly AppDbContext context;

        public Repositorio(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Existe(int id)
        {
            bool existe = await context.Set<E>()
                                       .AnyAsync(x => x.Id == id);
            return existe;
        }
        public async Task<bool> ExistePredi(Expression<Func<E, bool>> predicate)
        {
            return await context.Set<E>().AnyAsync(predicate);
        }
        public async Task<List<E>> Select()
        {
            return await context.Set<E>().ToListAsync();
        }

        public async Task<E?> SelectById(int id)
        {
            return await context.Set<E>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Insert(E entidad)
        {
            try
            {
                //entidad.EstadoRegistro = EnumEstadoRegistro.activo;
                await context.Set<E>().AddAsync(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception err) { throw err; }
        }

        public async Task<bool> Update(int id, E entidad)
        {
            if (id != entidad.Id) return false;

            var existe = await Existe(id);
            if (!existe) return false;

            try
            {
                context.Set<E>().Update(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception err) { throw err; }
        }
        public async Task<bool> Delete(int id)
        {
            var entidad = await SelectById(id);
            if (entidad is null) return false;
            try
            {
                context.Set<E>().Remove(entidad);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception err) { throw err; }
        }
    }
}
