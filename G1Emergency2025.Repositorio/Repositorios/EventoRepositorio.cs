using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace G1Emergency2025.Repositorio.Repositorios
{
    public class EventoRepositorio : Repositorio<Evento>, IEventoRepositorio
    {
        private readonly AppDbContext context;
        private readonly IPacienteRepositorio pacienteRepo;
        private readonly IUsuarioRepositorio usuarioRepo;
        private readonly ILugarHechoRepositorio lugarHechoRepo;

        public EventoRepositorio(AppDbContext context,
        IPacienteRepositorio pacienteRepo,
        IUsuarioRepositorio usuarioRepo,
        ILugarHechoRepositorio lugarHechoRepo) : base(context)
        {
            this.context = context;
            this.pacienteRepo = pacienteRepo;
            this.usuarioRepo = usuarioRepo;
            this.lugarHechoRepo = lugarHechoRepo;
        }
        public async Task<EventoListadoDTO?> SelectPorFecha(DateTime fechaHora)
        {
            return await context.Eventos
                .Include(e => e.PacienteEventos)
                    .ThenInclude(pe => pe.Pacientes)
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Where(e => e.FechaHora == fechaHora)

                .Select(e => new EventoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    Pacientes = e.PacienteEventos
                        .Select(pe => new PacienteResumenDTO
                        {
                            Id = pe.PacienteId,
                            ObraSocial = pe.Pacientes!.ObraSocial,
                            NombrePersona = pe.Pacientes!.Persona!.Nombre,
                            DNIPersona = pe.Pacientes!.Persona!.DNI,
                            LegajoPersona = pe.Pacientes!.Persona!.Legajo,
                            DireccionPersona = pe.Pacientes!.Persona!.Direccion,
                            SexoPersona = pe.Pacientes!.Persona!.Sexo,
                            EdadPersona = pe.Pacientes!.Persona!.Edad,
                            HistoriaClinica = pe.Pacientes!.HistoriaClinica
                        }).ToList(),

                    Usuarios = e.EventoUsuarios
                        .Select(eu => new UsuarioResumenDTO
                        {
                            Id = eu.UsuarioId,
                            Nombre = eu.Usuarios!.Nombre,
                            Contrasena = eu.Usuarios.Contrasena
                        }).ToList(),

                    Lugares = e.EventoLugarHechos
                        .Select(elh => new LugarHechoResumenDTO
                        {
                            Id = elh.LugarHecho!.Id,
                            Codigo = elh.LugarHecho.Codigo,
                            Tipo = elh.LugarHecho.Tipo,
                            Descripcion = elh.LugarHecho.Descripcion
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<EventoListadoDTO>> SelectPorFechaFlexible(
                    int? anio = null,
                    int? mes = null,
                    int? dia = null,
                    int? hora = null)
        {
            var query = context.Eventos
                .Include(e => e.PacienteEventos).ThenInclude(pe => pe.Pacientes).ThenInclude(p => p.Persona)
                .Include(e => e.EventoUsuarios).ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos).ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .AsQueryable();

            if (anio.HasValue)
                query = query.Where(e => e.FechaHora.Year == anio.Value);

            if (mes.HasValue)
                query = query.Where(e => e.FechaHora.Month == mes.Value);

            if (dia.HasValue)
                query = query.Where(e => e.FechaHora.Day == dia.Value);

            if (hora.HasValue)
                query = query.Where(e => e.FechaHora.Hour == hora.Value);


            var eventos = await query.ToListAsync();

            return eventos.Select(e => new EventoListadoDTO
            {
                Id = e.Id,
                Codigo = e.Codigo,
                colorEvento = e.colorEvento,
                Ubicacion = e.Ubicacion,
                Telefono = e.Telefono,
                FechaHora = e.FechaHora,
                Causa = e.Causa!.posibleCausa,
                TipoEstado = e.TipoEstados!.Tipo,
                Pacientes = e.PacienteEventos.Select(pe => new PacienteResumenDTO
                {
                    Id = pe.PacienteId,
                    ObraSocial = pe.Pacientes!.ObraSocial,
                    NombrePersona = pe.Pacientes!.Persona!.Nombre,
                    DNIPersona = pe.Pacientes!.Persona!.DNI,
                    LegajoPersona = pe.Pacientes!.Persona!.Legajo,
                    DireccionPersona = pe.Pacientes!.Persona!.Direccion,
                    SexoPersona = pe.Pacientes!.Persona!.Sexo,
                    EdadPersona = pe.Pacientes!.Persona!.Edad,
                    HistoriaClinica = pe.Pacientes!.HistoriaClinica
                }).ToList(),
                Usuarios = e.EventoUsuarios.Select(eu => new UsuarioResumenDTO
                {
                    Id = eu.UsuarioId,
                    Nombre = eu.Usuarios!.Nombre,
                    Contrasena = eu.Usuarios.Contrasena
                }).ToList(),
                Lugares = e.EventoLugarHechos.Select(elh => new LugarHechoResumenDTO
                {
                    Id = elh.LugarHecho!.Id,
                    Codigo = elh.LugarHecho.Codigo,
                    Tipo = elh.LugarHecho.Tipo,
                    Descripcion = elh.LugarHecho.Descripcion
                }).ToList()
            }).ToList();
        }


        public async Task<EventoListadoDTO?> SelectPorCod(string cod)
        {
            return await context.Eventos
                .Include(e => e.PacienteEventos)
                    .ThenInclude(pe => pe.Pacientes)
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Where(e => e.Codigo == cod)

                .Select(e => new EventoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    Pacientes = e.PacienteEventos
                        .Select(pe => new PacienteResumenDTO
                        {
                            Id = pe.PacienteId,
                            ObraSocial = pe.Pacientes!.ObraSocial,
                            NombrePersona = pe.Pacientes!.Persona!.Nombre,
                            DNIPersona = pe.Pacientes!.Persona!.DNI,
                            LegajoPersona = pe.Pacientes!.Persona!.Legajo,
                            DireccionPersona = pe.Pacientes!.Persona!.Direccion,
                            SexoPersona = pe.Pacientes!.Persona!.Sexo,
                            EdadPersona = pe.Pacientes!.Persona!.Edad,
                            HistoriaClinica = pe.Pacientes!.HistoriaClinica
                        }).ToList(),

                    Usuarios = e.EventoUsuarios
                        .Select(eu => new UsuarioResumenDTO
                        {
                            Id = eu.UsuarioId,
                            Nombre = eu.Usuarios!.Nombre,
                            Contrasena = eu.Usuarios.Contrasena
                        }).ToList(),

                    Lugares = e.EventoLugarHechos
                        .Select(elh => new LugarHechoResumenDTO
                        {
                            Id = elh.LugarHecho!.Id,
                            Codigo = elh.LugarHecho.Codigo,
                            Tipo = elh.LugarHecho.Tipo,
                            Descripcion = elh.LugarHecho.Descripcion
                        }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<EventoListadoDTO>> SelectListaEventoReciente()
        {
            try
            {

                var hace24hs = DateTime.Now.AddHours(-24);

                var lista = await context.Eventos

                .Where(e => e.FechaHora >= hace24hs)
                .Include(e => e.PacienteEventos)
                    .ThenInclude(pe => pe.Pacientes)
                        .ThenInclude(p => p.Persona)
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Select(e => new EventoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    //Pacientes = e.PacienteEventos.Select(pe => pe.Paciente.Persona.Nombre).ToList(),
                    //Usuarios = e.EventoUsuarios.Select(eu => eu.Usuario.Nombre).ToList(),
                    //Lugares = e.EventoLugarHechos.Select(elh => elh.LugarHechoResumenDTO.Descripcion).ToList()
                    Pacientes = e.PacienteEventos
                        .Select(pe => new PacienteResumenDTO
                        {
                            Id = pe.PacienteId,
                            ObraSocial = pe.Pacientes!.ObraSocial,
                            NombrePersona = pe.Pacientes!.Persona!.Nombre,
                            DNIPersona = pe.Pacientes!.Persona!.DNI,
                            LegajoPersona = pe.Pacientes!.Persona!.Legajo,
                            DireccionPersona = pe.Pacientes!.Persona!.Direccion,
                            SexoPersona = pe.Pacientes!.Persona!.Sexo,
                            EdadPersona = pe.Pacientes!.Persona!.Edad,
                            HistoriaClinica = pe.Pacientes!.HistoriaClinica
                        }).ToList(),

                    Usuarios = e.EventoUsuarios
                        .Select(eu => new UsuarioResumenDTO
                        {
                            Id = eu.UsuarioId,
                            Nombre = eu.Usuarios!.Nombre,
                            Contrasena = eu.Usuarios.Contrasena
                        }).ToList(),

                    Lugares = e.EventoLugarHechos
                        .Select(elh => new LugarHechoResumenDTO
                        {
                            Id = elh.LugarHecho!.Id,
                            Codigo = elh.LugarHecho.Codigo,
                            Tipo = elh.LugarHecho.Tipo,
                            Descripcion = elh.LugarHecho.Descripcion
                        }).ToList()

                })
                .OrderBy(e => e.FechaHora)
                .ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR al traer la Lista de Eventos:");
                Console.WriteLine(ex);
                throw;
            }

        }

        public async Task<List<EventoListadoDTO>> SelectListaEvento()
        {

            var lista = await context.Eventos

            .Include(e => e.PacienteEventos)
                .ThenInclude(pe => pe.Pacientes)
                     .ThenInclude(p => p.Persona)
            .Include(e => e.EventoUsuarios)
                .ThenInclude(eu => eu.Usuarios)
            .Include(e => e.EventoLugarHechos)
                .ThenInclude(elh => elh.LugarHecho)
            .Include(e => e.TipoEstados)
            .Include(e => e.Causa)

            .Select(e => new EventoListadoDTO
            {
                Id = e.Id,
                Codigo = e.Codigo,
                colorEvento = e.colorEvento,
                Ubicacion = e.Ubicacion,
                Telefono = e.Telefono,
                FechaHora = e.FechaHora,
                Causa = e.Causa!.posibleCausa,
                TipoEstado = e.TipoEstados!.Tipo,
                Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteResumenDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        LegajoPersona = pe.Pacientes.Persona.Legajo,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica
                    }).ToList(),

                Usuarios = e.EventoUsuarios
                    .Select(eu => new UsuarioResumenDTO
                    {
                        Id = eu.UsuarioId,
                        Nombre = eu.Usuarios!.Nombre,
                        Contrasena = eu.Usuarios.Contrasena
                    }).ToList(),

                Lugares = e.EventoLugarHechos
                    .Select(elh => new LugarHechoResumenDTO
                    {
                        Id = elh.LugarHecho!.Id,
                        Codigo = elh.LugarHecho.Codigo,
                        Tipo = elh.LugarHecho.Tipo,
                        Descripcion = elh.LugarHecho.Descripcion
                    }).ToList()

            })
            .OrderBy(e => e.FechaHora)
            .ToListAsync();

            return lista;
        }

        public async Task<int> InsertarEvento(EventoDTO dto)
        {

            var evento = new Evento
            {
                Relato = dto.Relato,
                Codigo = await GenerarCodigoUnicoAsync(),
                colorEvento = dto.colorEvento,
                Ubicacion = dto.Ubicacion,
                Telefono = dto.Telefono,
                FechaHora = dto.FechaHora,
                CausaId = dto.CausaId,
                TipoEstadoId = dto.TipoEstadoId,
                EstadoRegistro = EnumEstadoRegistro.activo
            };

            context.Eventos.Add(evento);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)

            {
                // Si hubo colisión de código, regenerar y reintentar
                if (ex.InnerException?.Message.Contains("Evento_UQ") == true)
                {
                    evento.Codigo = await GenerarCodigoUnicoAsync();
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }

            if (dto.PacienteIds != null)
                foreach (var pid in dto.PacienteIds)
                    await pacienteRepo.AsociarEvento(pid, evento.Id);

            if (dto.UsuarioIds != null)
                foreach (var uid in dto.UsuarioIds)
                    await usuarioRepo.AsociarEvento(uid, evento.Id);

            if (dto.LugarHechoIds != null)
                foreach (var lid in dto.LugarHechoIds)
                    await lugarHechoRepo.AsociarEvento(lid, evento.Id);

            return evento.Id;
        }
        public async Task<bool> ActualizarEvento(int id, EventoDTO dto)
        {
            var evento = await context.Eventos.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
                return false;

            evento.Codigo = dto.Codigo;
            evento.Ubicacion = dto.Ubicacion;
            evento.Telefono = dto.Telefono;
            evento.FechaHora = dto.FechaHora;
            evento.colorEvento = dto.colorEvento;
            evento.CausaId = dto.CausaId;
            evento.TipoEstadoId = dto.TipoEstadoId;

            await context.SaveChangesAsync();

            var relacionesOk = await ActualizarRelacionesEvento(id, dto);

            return relacionesOk;
        }

        private async Task<string> GenerarCodigoUnicoAsync()
        {
            var random = new Random();
            string codigo;
            bool existe;

            do
            {
                // Genera un número aleatorio de 6 dígitos
                codigo = random.Next(100000, 999999).ToString();

                // Verifica si ya existe en la base
                existe = await context.Eventos.AnyAsync(e => e.Codigo == codigo);

            } while (existe);

            return codigo;
        }

        public async Task<bool> ActualizarRelacionesEvento(int id, EventoDTO dto)
        {
            var evento = await context.Eventos
                .Include(e => e.PacienteEventos)
                .Include(e => e.EventoUsuarios)
                .Include(e => e.EventoLugarHechos)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null) return false;

            // 🔹 Actualizar relaciones Pacientes
            evento.PacienteEventos.Clear();
            if (dto.PacienteIds != null && dto.PacienteIds.Any())
            {
                foreach (var pid in dto.PacienteIds)
                    evento.PacienteEventos.Add(new PacienteEvento { PacienteId = pid, EventoId = id });
            }

            // 🔹 Actualizar relaciones Usuarios
            evento.EventoUsuarios.Clear();
            if (dto.UsuarioIds != null && dto.UsuarioIds.Any())
            {
                foreach (var uid in dto.UsuarioIds)
                    evento.EventoUsuarios.Add(new EventoUsuario { UsuarioId = uid, EventoId = id });
            }

            // 🔹 Actualizar relaciones Lugares
            evento.EventoLugarHechos.Clear();
            if (dto.LugarHechoIds != null && dto.LugarHechoIds.Any())
            {
                foreach (var lid in dto.LugarHechoIds)
                    evento.EventoLugarHechos.Add(new EventoLugarHecho { LugarHechoId = lid, EventoId = id });
            }

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEvento(int id)
        {
            var evento = await context.Eventos
                .Include(e => e.PacienteEventos)
                .Include(e => e.EventoUsuarios)
                .Include(e => e.EventoLugarHechos)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
                return false;

            // Eliminar relaciones N:M manualmente
            context.RemoveRange(evento.PacienteEventos);
            context.RemoveRange(evento.EventoUsuarios);
            context.RemoveRange(evento.EventoLugarHechos);

            // Finalmente eliminar el evento
            context.Eventos.Remove(evento);

            await context.SaveChangesAsync();
            return true;
        }
    }
}
