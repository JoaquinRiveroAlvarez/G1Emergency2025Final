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
    public class TripulanteRepositorio : Repositorio<Tripulante>, ITripulanteRepositorio
    {
        private readonly AppDbContext context;

        public TripulanteRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Tripulante?> SelectByEnMovil(bool EnMovil)
        {
            return await context.Set<Tripulante>().FirstOrDefaultAsync(x => x.EnMovil == EnMovil);
        }
        public async Task<List<TripulanteListadoDTO>> SelectListaTripulante()
        {
            var lista = await context.Tripulantes
            .Include(p => p.Personas)
            .Include(p => p.TipoTripulantes)
            .Select(p => new TripulanteListadoDTO
            {
                Id = p.Id,
                Tipo = p.TipoTripulantes!.Tipo,
                EnMovil = p.EnMovil,
                Nombre = p.Personas!.Nombre,
                DNI = p.Personas.DNI,
                Legajo = p.Personas.Legajo,
                Edad = p.Personas.Edad,
                Direccion = p.Personas.Direccion,
                Sexo = p.Personas.Sexo
            })
            .ToListAsync();
            return lista;
        }
        public async Task<int> InsertarTripulanteConPersona(TripulanteConPersonaDTO dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear Persona
                var persona = new Persona
                {
                    Nombre = dto.Nombre,
                    DNI = dto.DNI,
                    Legajo = dto.Legajo,
                    Edad = dto.Edad,
                    Direccion = dto.Direccion,
                    Sexo = dto.Sexo
                };

                await context.Persona.AddAsync(persona);
                await context.SaveChangesAsync();

                // 2. Buscar TipoTripulante por Código
                var tipoTripulante = await context.TipoTripulantes
                    .FirstOrDefaultAsync(t => t.Tipo == dto.TipoDelTripulante);

                if (tipoTripulante == null)
                    throw new Exception($"No existe un TipoTripulante con código {dto.TipoDelTripulante}");

                // 3. Crear Tripulante
                var tripulante = new Tripulante
                {
                    EnMovil = false,
                    PersonaId = persona.Id,
                    TipoTripulanteId = tipoTripulante.Id
                };

                await context.Tripulantes.AddAsync(tripulante);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
                return tripulante.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task ActualizarEstadoEnMovil(int tripulanteId, bool enMovil)
        {
            var tripulante = await context.Tripulantes.FindAsync(tripulanteId);
            if (tripulante == null)
                throw new Exception($"Tripulante con ID {tripulanteId} no encontrado.");

            tripulante.EnMovil = enMovil;
            await context.SaveChangesAsync();
        }
    }
}
