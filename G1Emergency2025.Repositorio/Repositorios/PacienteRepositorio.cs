using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace G1Emergency2025.Repositorio.Repositorios
{
    public class PacienteRepositorio : Repositorio<Paciente>, IPacienteRepositorio
    {
        private readonly AppDbContext context;

        public PacienteRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Paciente?> SelectByObraSocial(string cod)
        {
            return await context.Set<Paciente>().FirstOrDefaultAsync(x => x.ObraSocial == cod);
        }
        public async Task<List<PacienteResumenDTO>> SelectListaPaciente()
        {
            var lista = await context.Pacientes
                .Include(p => p.Persona)
                                    .Select(p => new PacienteResumenDTO
                                    {
                                        Id = p.Id,
                                        ObraSocial = p.ObraSocial,
                                        NombrePersona = p.Persona!.Nombre,
                                        DNIPersona = p.Persona!.DNI,
                                        LegajoPersona = p.Persona!.Legajo,
                                        DireccionPersona = p.Persona!.Direccion,
                                        EdadPersona = p.Persona!.Edad,
                                        SexoPersona = p.Persona!.Sexo,
                                        HistoriaClinica = p.HistoriaClinica
                                    })
                                    .ToListAsync();
            return lista;
        }
        public async Task AsociarEvento(int pacienteId, int eventoId)
        {
            var rel = new PacienteEvento
            {
                PacienteId = pacienteId,
                EventoId = eventoId
            };

            context.PacienteEventos.Add(rel);
            await context.SaveChangesAsync();
        }
        public async Task<bool> UpdatePacienteConEventos(int id, PacienteActualizarDTO dto)
        {
            // 1. Buscar paciente con relaciones
            var paciente = await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos) // Tabla intermedia
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
                return false;

            // 2. Actualizar datos del paciente
            paciente.ObraSocial = dto.ObraSocial!;

            // 3. Actualizar datos de persona
            if (paciente.Persona != null)
            {
                paciente.Persona.Nombre = dto.PersonaDTO.Nombre;
                paciente.Persona.DNI = dto.PersonaDTO.DNI;
                paciente.Persona.Direccion = dto.PersonaDTO.Direccion;
                paciente.Persona.Sexo = dto.PersonaDTO.sexo;
                paciente.Persona.Edad = dto.PersonaDTO.Edad;
            }

            // 4. Actualizar relaciones N:M
            var eventosActuales = paciente.PacienteEventos.Select(pe => pe.EventoId).ToList();

            // Eventos a agregar
            var eventosNuevos = dto.EventosIds.Except(eventosActuales).ToList();
            foreach (var eventoId in eventosNuevos)
            {
                paciente.PacienteEventos.Add(new PacienteEvento
                {
                    PacienteId = id,
                    EventoId = eventoId
                });
            }

            // Eventos a eliminar
            var eventosAEliminar = eventosActuales.Except(dto.EventosIds).ToList();
            foreach (var eventoId in eventosAEliminar)
            {
                var relacion = paciente.PacienteEventos.FirstOrDefault(pe => pe.EventoId == eventoId);
                if (relacion != null)
                    context.PacienteEventos.Remove(relacion);
            }

            // 5. Guardar cambios
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CrearPacienteConPersona(Persona persona, Paciente paciente, int eventoId)
        {
            var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                await context.Persona.AddAsync(persona);
                await context.SaveChangesAsync();

                paciente.PersonaId = persona.Id;
                await context.Pacientes.AddAsync(paciente);
                await context.SaveChangesAsync();

                await AsociarEvento(paciente.Id, eventoId);
                await transaction.CommitAsync();
                return paciente.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<Paciente?> ObtenerConPersonaAsync(int id)
        {
            return await context.Pacientes
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
