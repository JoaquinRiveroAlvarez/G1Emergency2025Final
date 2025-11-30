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
                DireccionPersona = p.Persona!.Direccion,
                EdadPersona = p.Persona!.Edad,
                SexoPersona = p.Persona!.Sexo,
                HistoriaClinica = p.HistoriaClinica
            })
            .ToListAsync();
            return lista;
        }
        public async Task<List<PacienteResumenDTO>> SelectListaPacienteConPersonaYEvento()
        {
            var lista = await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Select(p => new PacienteResumenDTO
                {
                    Id = p.Id,
                    ObraSocial = p.ObraSocial,
                    NombrePersona = p.Persona!.Nombre,
                    DNIPersona = p.Persona!.DNI,
                    DireccionPersona = p.Persona!.Direccion,
                    EdadPersona = p.Persona!.Edad,
                    SexoPersona = p.Persona!.Sexo,
                    HistoriaClinica = p.HistoriaClinica,

                    Eventos = p.PacienteEventos
                        .Select(pe => new EventoPacienteMostrarDTO
                        {
                            EventoId = pe.EventoId,
                            CodigoEvento = pe.Eventos!.Codigo,
                            DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
                        })
                        .ToList()
                })
                .ToListAsync();
                return lista;
        }
        public async Task<PacienteResumenDTO?> SelectPacienteConPersonaYEvento(int id)
        {
            return await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Where(p => p.Id == id)
                .Select(p => new PacienteResumenDTO
                {
                    Id = p.Id,
                    ObraSocial = p.ObraSocial,
                    NombrePersona = p.Persona!.Nombre,
                    DNIPersona = p.Persona!.DNI,
                    DireccionPersona = p.Persona!.Direccion,
                    EdadPersona = p.Persona!.Edad,
                    SexoPersona = p.Persona!.Sexo,
                    HistoriaClinica = p.HistoriaClinica,

                    Eventos = p.PacienteEventos
                        .Select(pe => new EventoPacienteMostrarDTO
                        {
                            EventoId = pe.EventoId,
                            CodigoEvento = pe.Eventos!.Codigo,
                            DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task AsociarEvento(int pacienteId, int eventoId, string diagnosticoPresuntivo)
        {
            var rel = new PacienteEvento
            {
                PacienteId = pacienteId,
                EventoId = eventoId,
                DiagnosticoPresuntivo = string.Empty
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
            paciente.HistoriaClinica = dto.HistoriaClinica!;

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
            var eventosNuevos = dto.Eventos.Select(e => e.EventoId).Except(eventosActuales).ToList();
            foreach (var eventoId in eventosNuevos)
            {
                var diagPresuntivo = dto.Eventos.First(e => e.EventoId == eventoId).DiagnosticoPresuntivo;
                paciente.PacienteEventos.Add(new PacienteEvento
                {
                    PacienteId = id,
                    EventoId = eventoId,
                    DiagnosticoPresuntivo = diagPresuntivo
                });
            }
            // Eventos a actualizar (diagnóstico)
            foreach (var relacion in paciente.PacienteEventos)
            {
                var dtoEvento = dto.Eventos.FirstOrDefault(e => e.EventoId == relacion.EventoId);
                if (dtoEvento != null)
                {
                    relacion.DiagnosticoPresuntivo = dtoEvento.DiagnosticoPresuntivo;
                }
            }
            // Eventos a eliminar
            var eventosAEliminar = eventosActuales.Except(dto.Eventos.Select(e => e.EventoId)).ToList();
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

        public async Task<int> CrearPacienteConPersona(PacienteCrearDTO dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear Persona
                var persona = new Persona
                {
                    Nombre = dto.Persona.Nombre,
                    DNI = dto.Persona.DNI,
                    Legajo = dto.Persona.Legajo,
                    Direccion = dto.Persona.Direccion,
                    Sexo = dto.Persona.Sexo,
                    Edad = dto.Persona.Edad
                };
                await context.Persona.AddAsync(persona);
                await context.SaveChangesAsync();

                // 2. Crear Paciente
                var paciente = new Paciente
                {
                    ObraSocial = dto.ObraSocial,
                    HistoriaClinica = dto.HistoriaClinica,
                    PersonaId = persona.Id
                };
                await context.Pacientes.AddAsync(paciente);
                await context.SaveChangesAsync();

                // 3. Asociar eventos con diagnóstico presuntivo
                foreach (var ev in dto.Eventos)
                {
                    var rel = new PacienteEvento
                    {
                        PacienteId = paciente.Id,
                        EventoId = ev.EventoId,
                        DiagnosticoPresuntivo = ev.DiagnosticoPresuntivo
                    };
                    context.PacienteEventos.Add(rel);
                }
                await context.SaveChangesAsync();

                // 4. Confirmar transacción
                await transaction.CommitAsync();
                return paciente.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
