using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
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
        public async Task<PacienteResumenDTO?> SelectPorId(int pacienteId)
        {
            var paciente = await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Where(p => p.Id == pacienteId)
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

            return paciente;
        }
        public async Task<PacienteDTO?> SelectPorDNIPersona(int pacienteId)
        {
            var paciente = await context.Pacientes
                .Include(p => p.Persona)
                .Where(p => p.Id == pacienteId)
                .Select(p => new PacienteDTO
                {
                    Id = p.Id,
                    ObraSocial = p.ObraSocial,
                    HistoriaClinica = p.HistoriaClinica,
                    PersonaDTO = new PersonaDTO
                    {
                        Nombre = p.Persona!.Nombre,
                        DNI = p.Persona.DNI,
                        Direccion = p.Persona.Direccion,
                        Sexo = p.Persona.Sexo,
                        Edad = p.Persona.Edad
                    }
                })
                .FirstOrDefaultAsync();

            return paciente;
        }
        public async Task<PacienteResumenDTO?> SelectByObraSocial(string cod)
        {
            return await context.Set<PacienteResumenDTO>().FirstOrDefaultAsync(x => x.ObraSocial == cod);
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
        public async Task<PacienteResumenDTO?> SelectNombrePacienteConPersonaYEvento(string nombre)
        {
            return await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Where(p => p.Persona!.Nombre == nombre)
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
        public async Task<PacienteResumenDTO?> SelectDNIPacienteConPersonaYEvento(string cod)
        {
            return await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Where(p => p.Persona!.DNI == cod)
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
        public async Task<PacienteResumenDTO?> SelectHistoriaClinicaPacienteConPersonaYEvento(string historiaClinica)
        {
            return await context.Pacientes
                .Include(p => p.Persona)
                .Include(p => p.PacienteEventos)
                    .ThenInclude(pe => pe.Eventos)
                .Where(p => p.HistoriaClinica == historiaClinica)
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
        private async Task<string> GenerarCodigoUnico()
        {
            var random = new Random();
            string historiaClinica;
            bool existe;

            do
            {
                // Genera un número aleatorio de 6 dígitos
                historiaClinica = random.Next(100000, 999999).ToString();

                // Verifica si ya existe en la base
                existe = await context.Pacientes.AnyAsync(e => e.HistoriaClinica == historiaClinica);

            } while (existe);

            return historiaClinica;
        }
        public async Task AsociarEvento(int pacienteId, int eventoId, string diagnosticoPresuntivo)
        {
            var rel = new PacienteEvento
            {
                EstadoRegistro = EnumEstadoRegistro.activo,
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

            // Si el DTO trae historia clínica la usamos, si no, mantenemos la existente
            paciente.HistoriaClinica = string.IsNullOrWhiteSpace(dto.HistoriaClinica)
                ? paciente.HistoriaClinica
                : dto.HistoriaClinica;


            // 3. Actualizar datos de persona
            if (paciente.Persona != null)
            {
                paciente.Persona.Nombre = dto.PersonaDTO.Nombre;
                paciente.Persona.DNI = dto.PersonaDTO.DNI;
                paciente.Persona.Direccion = dto.PersonaDTO.Direccion;
                paciente.Persona.Sexo = dto.PersonaDTO.Sexo;
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

                // 2. Crear Persona
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

                // 3. Crear Paciente
                var paciente = new Paciente
                {
                    HistoriaClinica = await GenerarCodigoUnico(),
                    ObraSocial = dto.ObraSocial,
                    PersonaId = persona.Id
                };
                await context.Pacientes.AddAsync(paciente);
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

        //public async Task<int> CrearPacienteConPersona(PacienteCrearDTO dto)
        //{
        //    using var transaction = await context.Database.BeginTransactionAsync();
        //    try
        //    {
        //        // 1. Convertir Sexo
        //        Sexo sexoConvertido = Enum.Parse<Sexo>(dto.Persona.Sexo);

        //        // 2. Crear Persona
        //        var persona = new Persona
        //        {
        //            Nombre = dto.Persona.Nombre,
        //            DNI = dto.Persona.DNI,
        //            Legajo = dto.Persona.Legajo,
        //            Direccion = dto.Persona.Direccion,
        //            Sexo = sexoConvertido,
        //            Edad = dto.Persona.Edad
        //        };
        //        await context.Persona.AddAsync(persona);
        //        await context.SaveChangesAsync();

        //        // 3. Crear Paciente
        //        var paciente = new Paciente
        //        {
        //            HistoriaClinica = await GenerarCodigoUnico(),
        //            ObraSocial = dto.ObraSocial,
        //            PersonaId = persona.Id
        //        };
        //        await context.Pacientes.AddAsync(paciente);
        //        await context.SaveChangesAsync();

        //        // 4. Confirmar transacción
        //        await transaction.CommitAsync();
        //        return paciente.Id;
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        await transaction.RollbackAsync();

        //        // Reintento por colisión de código único
        //        if (ex.InnerException?.Message.Contains("Paciente_UQ") == true)
        //        {
        //            var nuevoCodigo = await GenerarCodigoUnico();
        //            paciente.HistoriaClinica = nuevoCodigo;
        //            await context.SaveChangesAsync();
        //            return paciente.Id;
        //        }

        //        throw;
        //    }
        //    catch
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}

    }
}
