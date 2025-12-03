using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
        private readonly IMovilRepositorio movilRepo;

        public EventoRepositorio(AppDbContext context,
        IPacienteRepositorio pacienteRepo,
        IUsuarioRepositorio usuarioRepo,
        ILugarHechoRepositorio lugarHechoRepo,
        IMovilRepositorio movilRepo) : base(context)
        {
            this.context = context;
            this.pacienteRepo = pacienteRepo;
            this.usuarioRepo = usuarioRepo;
            this.lugarHechoRepo = lugarHechoRepo;
            this.movilRepo = movilRepo;
        }
        
        public async Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorTipoEstado(int estadoEventoId)
        {
            var lista = await context.Eventos

            .Include(e => e.PacienteEventos)
                .ThenInclude(pe => pe.Pacientes)
            .Include(e => e.EventoUsuarios)
                .ThenInclude(eu => eu.Usuarios)
            .Include(e => e.EventoLugarHechos)
                .ThenInclude(elh => elh.LugarHecho)
            .Include(e => e.EventoMovils)
                .ThenInclude(em => em.Movil)
            .Include(e => e.TipoEstados)
            .Include(e => e.Causa)
            .Where(e => e.TipoEstadoId == estadoEventoId)

            .Select(e => new EventoDiagPresuntivoListadoDTO
            {
                Id = e.Id,
                Codigo = e.Codigo,
                colorEvento = e.colorEvento,
                Ubicacion = e.Ubicacion,
                Telefono = e.Telefono,
                FechaHora = e.FechaHora,
                Causa = e.Causa!.posibleCausa,
                TipoEstado = e.TipoEstados!.Tipo,
                TipoEstadoId = e.TipoEstadoId,
                Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteDiagPresuntivoDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                        DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
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
                    }).ToList(),

                Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                {
                    Id = em.Movil!.Id,
                    Patente = em.Movil.Patente,
                    TipoMovil = em.Movil.TipoMovils!.Tipo,
                    disponibilidadMovil = em.Movil.disponibilidadMovil
                }).ToList()

            })
            .OrderBy(e => e.FechaHora)
            .ToListAsync();
            return lista;
        }
        public async Task<List<EventoDiagPresuntivoListadoDTO>> SelectPorNombrePaciente(string nombrePaciente)
        {
            var lista = await context.Eventos

            .Include(e => e.PacienteEventos)
                .ThenInclude(pe => pe.Pacientes)
                .ThenInclude(p => p.Persona)
            .Include(e => e.EventoUsuarios)
                .ThenInclude(eu => eu.Usuarios)
            .Include(e => e.EventoLugarHechos)
                .ThenInclude(elh => elh.LugarHecho)
            .Include(e => e.EventoMovils)
                .ThenInclude(em => em.Movil)
            .Include(e => e.TipoEstados)
            .Include(e => e.Causa)
                 .Where(e => e.PacienteEventos
                     .Any(pe => pe.Pacientes != null &&
                                pe.Pacientes.Persona != null &&
                                EF.Functions.Like(pe.Pacientes.Persona.Nombre, $"%{nombrePaciente}%")))



            .Select(e => new EventoDiagPresuntivoListadoDTO
            {
                Id = e.Id,
                Codigo = e.Codigo,
                colorEvento = e.colorEvento,
                Ubicacion = e.Ubicacion,
                Telefono = e.Telefono,
                FechaHora = e.FechaHora,
                Causa = e.Causa!.posibleCausa,
                TipoEstado = e.TipoEstados!.Tipo,
                TipoEstadoId = e.TipoEstadoId,
                Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteDiagPresuntivoDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                        DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
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
                    }).ToList(),

                Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                {
                    Id = em.Movil!.Id,
                    Patente = em.Movil.Patente,
                    TipoMovil = em.Movil.TipoMovils!.Tipo,
                    disponibilidadMovil = em.Movil.disponibilidadMovil
                }).ToList()

            })
            .OrderBy(e => e.FechaHora)
            .ToListAsync();
            return lista;
        }
        //public async Task<List<EventoListadoDTO>> SelectPorHistoriaClinicaPaciente(string historiaClinica)
        //{
        //    var lista = await context.Eventos

        //    .Include(e => e.PacienteEventos)
        //        .ThenInclude(pe => pe.Pacientes)
        //        .ThenInclude(p => p.Persona)
        //    .Include(e => e.EventoUsuarios)
        //        .ThenInclude(eu => eu.Usuarios)
        //    .Include(e => e.EventoLugarHechos)
        //        .ThenInclude(elh => elh.LugarHecho)
        //    .Include(e => e.EventoMovils)
        //        .ThenInclude(em => em.Movil)
        //    .Include(e => e.TipoEstados)
        //    .Include(e => e.Causa)
        //    .Where(e => e.PacienteEventos
        //    .Select(e => new EventoListadoDTO
        //    {
        //        Id = e.Id,
        //        Codigo = e.Codigo,
        //        colorEvento = e.colorEvento,
        //        Ubicacion = e.Ubicacion,
        //        Telefono = e.Telefono,
        //        FechaHora = e.FechaHora,
        //        Causa = e.Causa!.posibleCausa,
        //        TipoEstado = e.TipoEstados!.Tipo,
        //        TipoEstadoId = e.TipoEstadoId,
        //        Pacientes = e.PacienteEventos
        //            .Select(pe => new PacienteResumenDTO
        //            {
        //                Id = pe.PacienteId,
        //                ObraSocial = pe.Pacientes!.ObraSocial,
        //                NombrePersona = pe.Pacientes.Persona!.Nombre,
        //                DNIPersona = pe.Pacientes.Persona.DNI,
        //                DireccionPersona = pe.Pacientes!.Persona.Direccion,
        //                SexoPersona = pe.Pacientes.Persona.Sexo,
        //                EdadPersona = pe.Pacientes.Persona.Edad,
        //                HistoriaClinica = pe.Pacientes!.HistoriaClinica
        //            }).ToList(),

        //        Usuarios = e.EventoUsuarios
        //            .Select(eu => new UsuarioResumenDTO
        //            {
        //                Id = eu.UsuarioId,
        //                Nombre = eu.Usuarios!.Nombre,
        //                Contrasena = eu.Usuarios.Contrasena
        //            }).ToList(),

        //        Lugares = e.EventoLugarHechos
        //            .Select(elh => new LugarHechoResumenDTO
        //            {
        //                Id = elh.LugarHecho!.Id,
        //                Codigo = elh.LugarHecho.Codigo,
        //                Tipo = elh.LugarHecho.Tipo,
        //                Descripcion = elh.LugarHecho.Descripcion
        //            }).ToList(),

        //        Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
        //        {
        //            Id = em.Movil!.Id,
        //            Patente = em.Movil.Patente,
        //            TipoMovil = em.Movil.TipoMovils!.Tipo,
        //            disponibilidadMovil = em.Movil.disponibilidadMovil
        //        }).ToList()

        //    })
        //    .OrderBy(e => e.FechaHora)
        //    .ToListAsync();
        //    return lista;
        //}
        public async Task<List<EventoListadoDTO>> SelectPorFechaFlexible(
                    int? anio = null,
                    int? mes = null,
                    int? dia = null,
                    int? hora = null)
        {
            var query = context.Eventos
                .Include(e => e.PacienteEventos)
                    .ThenInclude(pe => pe.Pacientes)
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.EventoMovils)
                    .ThenInclude(em => em.Movil)
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
                TipoEstadoId = e.TipoEstadoId,
                Pacientes = e.PacienteEventos.Select(pe => new PacienteResumenDTO
                {
                    Id = pe.PacienteId,
                    ObraSocial = pe.Pacientes!.ObraSocial,
                    NombrePersona = pe.Pacientes!.Persona!.Nombre,
                    DNIPersona = pe.Pacientes!.Persona!.DNI,
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
                }).ToList(),

                Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                {
                    Id = em.Movil!.Id,
                    Patente = em.Movil.Patente,
                    TipoMovil = em.Movil.TipoMovils!.Tipo,
                    disponibilidadMovil = em.Movil.disponibilidadMovil
                }).ToList()

            }).ToList();
        }
        public async Task<EventoPacienteDiagPresuntivoResumenDTO?> SelectEventoConPacientePorId(int id)
        {
            return await context.Eventos

            .Include(e => e.PacienteEventos)
                .ThenInclude(pe => pe.Pacientes)
            .Where(e => e.Id == id)

            .Select(e => new EventoPacienteDiagPresuntivoResumenDTO
            {
                Id = e.Id,
                Codigo = e.Codigo,
                Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteDiagPresuntivoDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                        DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
                    }).ToList(),
            })
            .FirstOrDefaultAsync();
        }

        public async Task<EventoDiagPresuntivoListadoDTO?> SelectPorCod(string cod)
        {
            return await context.Eventos
                .Include(e => e.PacienteEventos)
                    .ThenInclude(pe => pe.Pacientes)
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.EventoMovils)
                    .ThenInclude(em => em.Movil)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Where(e => e.Codigo == cod)

                .Select(e => new EventoDiagPresuntivoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    TipoEstadoId = e.TipoEstadoId,
                    Pacientes = e.PacienteEventos
                        .Select(pe => new PacienteDiagPresuntivoDTO
                        {
                            Id = pe.PacienteId,
                            ObraSocial = pe.Pacientes!.ObraSocial,
                            NombrePersona = pe.Pacientes!.Persona!.Nombre,
                            DNIPersona = pe.Pacientes!.Persona!.DNI,
                            DireccionPersona = pe.Pacientes!.Persona!.Direccion,
                            SexoPersona = pe.Pacientes!.Persona!.Sexo,
                            EdadPersona = pe.Pacientes!.Persona!.Edad,
                            HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                            DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
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
                        }).ToList(),
                    Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                    {
                        Id = em.Movil!.Id,
                        Patente = em.Movil.Patente,
                        TipoMovil = em.Movil.TipoMovils!.Tipo,
                        disponibilidadMovil = em.Movil.disponibilidadMovil
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
                .Include(e => e.EventoUsuarios)
                    .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                    .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.EventoMovils)
                    .ThenInclude(em => em.Movil)
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
                    TipoEstadoId = e.TipoEstadoId,
                    Pacientes = e.PacienteEventos
                        .Select(pe => new PacienteResumenDTO
                        {
                            Id = pe.PacienteId,
                            ObraSocial = pe.Pacientes!.ObraSocial,
                            NombrePersona = pe.Pacientes!.Persona!.Nombre,
                            DNIPersona = pe.Pacientes!.Persona!.DNI,
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
                        }).ToList(),

                    Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                    {
                        Id = em.Movil!.Id,
                        Patente = em.Movil.Patente,
                        TipoMovil = em.Movil.TipoMovils!.Tipo,
                        disponibilidadMovil = em.Movil.disponibilidadMovil
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
            .Include(e => e.EventoUsuarios)
                .ThenInclude(eu => eu.Usuarios)
            .Include(e => e.EventoLugarHechos)
                .ThenInclude(elh => elh.LugarHecho)
            .Include(e => e.EventoMovils)
                .ThenInclude(em => em.Movil)
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
                TipoEstadoId = e.TipoEstadoId,
                Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteResumenDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
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
                    }).ToList(),

                Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                {
                    Id = em.Movil!.Id,
                    Patente = em.Movil.Patente,
                    TipoMovil = em.Movil.TipoMovils!.Tipo,
                    disponibilidadMovil = em.Movil.disponibilidadMovil
                }).ToList()

            })
            .OrderBy(e => e.FechaHora)
            .ToListAsync();

            return lista;
        }

        public async Task<List<EventoDiagPresuntivoListadoDTO>> SelectListaEventoCompleto()
        {
            var lista = await context.Eventos
                .Where(e => e.TipoEstados!.Codigo == "01" && e.EstadoRegistro == EnumEstadoRegistro.activo)
                .Include(e => e.PacienteEventos)
                   .ThenInclude(pe => pe.Pacientes)
                   .ThenInclude(p => p!.Persona)
                .Include(e => e.EventoUsuarios)
                   .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                   .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Include(e => e.EventoMovils)
                   .ThenInclude(em => em.Movil)
                   .ThenInclude(m => m!.TipoMovils)
                .Select(e => new EventoDiagPresuntivoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    TipoEstadoId = e.TipoEstadoId,

                    Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteDiagPresuntivoDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                        DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
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
                    }).ToList(),

                    Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                    {
                        Id = em.Movil!.Id,
                        Patente = em.Movil.Patente,
                        TipoMovil = em.Movil.TipoMovils!.Tipo,
                        disponibilidadMovil = em.Movil.disponibilidadMovil
                    }).ToList()
                })
                .OrderBy(e => e.FechaHora)
                .ToListAsync();

            return lista;
        }

        public async Task<List<EventoDiagPresuntivoListadoDTO>> SelectListaEventoRecienteCompleto()
        {
            var hace24hs = DateTime.Now.AddHours(-24);

            var lista = await context.Eventos

                .Where(e => e.FechaHora >= hace24hs && e.TipoEstados!.Codigo == "01" && e.EstadoRegistro == EnumEstadoRegistro.activo)
                .Include(e => e.PacienteEventos)
                   .ThenInclude(pe => pe.Pacientes)
                   .ThenInclude(p => p!.Persona)
                .Include(e => e.EventoUsuarios)
                   .ThenInclude(eu => eu.Usuarios)
                .Include(e => e.EventoLugarHechos)
                   .ThenInclude(elh => elh.LugarHecho)
                .Include(e => e.TipoEstados)
                .Include(e => e.Causa)
                .Include(e => e.EventoMovils)
                   .ThenInclude(em => em.Movil)
                   .ThenInclude(m => m!.TipoMovils)
                .Select(e => new EventoDiagPresuntivoListadoDTO
                {
                    Id = e.Id,
                    Codigo = e.Codigo,
                    colorEvento = e.colorEvento,
                    Ubicacion = e.Ubicacion,
                    Telefono = e.Telefono,
                    FechaHora = e.FechaHora,
                    Causa = e.Causa!.posibleCausa,
                    TipoEstado = e.TipoEstados!.Tipo,
                    TipoEstadoId = e.TipoEstadoId,

                    Pacientes = e.PacienteEventos
                    .Select(pe => new PacienteDiagPresuntivoDTO
                    {
                        Id = pe.PacienteId,
                        ObraSocial = pe.Pacientes!.ObraSocial,
                        NombrePersona = pe.Pacientes.Persona!.Nombre,
                        DNIPersona = pe.Pacientes.Persona.DNI,
                        DireccionPersona = pe.Pacientes!.Persona.Direccion,
                        SexoPersona = pe.Pacientes.Persona.Sexo,
                        EdadPersona = pe.Pacientes.Persona.Edad,
                        HistoriaClinica = pe.Pacientes!.HistoriaClinica,
                        DiagnosticoPresuntivo = pe.DiagnosticoPresuntivo
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
                    }).ToList(),

                    Moviles = e.EventoMovils.Select(em => new MovilResumenDTO
                    {
                        Id = em.Movil!.Id,
                        Patente = em.Movil.Patente,
                        TipoMovil = em.Movil.TipoMovils!.Tipo,
                        disponibilidadMovil = em.Movil.disponibilidadMovil
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
                    await pacienteRepo.AsociarEvento(pid, evento.Id, "Sin Diagnostico");

            if (dto.UsuarioIds != null)
                foreach (var uid in dto.UsuarioIds)
                    await usuarioRepo.AsociarEvento(uid, evento.Id);

            if (dto.LugarHechoIds != null)
                foreach (var lid in dto.LugarHechoIds)
                    await lugarHechoRepo.AsociarEvento(lid, evento.Id);
            if (dto.MovilIds != null)
                foreach (var mid in dto.MovilIds)
                    await movilRepo.AsociarEvento(mid, evento.Id);

            return evento.Id;
        }

        public async Task<int> InsertarEventoPaciente(EventoCrearDTO dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear Evento
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
                await context.SaveChangesAsync();

                // 2. Crear Pacientes asociados al evento
                if (dto.Pacientes != null)
                {
                    foreach (var pacienteDto in dto.Pacientes)
                    {
                        // Crear Persona
                        Sexo sexoConvertido = Enum.Parse<Sexo>(pacienteDto.Persona.Sexo);
                        var persona = new Persona
                        {
                            Nombre = pacienteDto.Persona.Nombre,
                            DNI = pacienteDto.Persona.DNI,
                            Legajo = pacienteDto.Persona.Legajo,
                            Direccion = pacienteDto.Persona.Direccion,
                            Sexo = sexoConvertido,
                            Edad = pacienteDto.Persona.Edad
                        };
                        await context.Persona.AddAsync(persona);
                        await context.SaveChangesAsync();

                        // Crear Paciente
                        var paciente = new Paciente
                        {
                            HistoriaClinica = await GenerarCodigoUnico(),
                            ObraSocial = pacienteDto.ObraSocial,
                            PersonaId = persona.Id
                        };
                        await context.Pacientes.AddAsync(paciente);
                        await context.SaveChangesAsync();

                        // Asociar Paciente al Evento
                        context.PacienteEventos.Add(new PacienteEvento
                        {
                            PacienteId = paciente.Id,
                            EventoId = evento.Id,
                            DiagnosticoPresuntivo = ""
                        });
                    }
                    await context.SaveChangesAsync();
                }

                // 3. Asociar otros elementos (usuarios, lugares, móviles)
                if (dto.UsuarioIds != null)
                    foreach (var uid in dto.UsuarioIds)
                        await usuarioRepo.AsociarEvento(uid, evento.Id);

                if (dto.LugarHechoIds != null)
                    foreach (var lid in dto.LugarHechoIds)
                        await lugarHechoRepo.AsociarEvento(lid, evento.Id);

                if (dto.MovilIds != null)
                    foreach (var mid in dto.MovilIds)
                        await movilRepo.AsociarEvento(mid, evento.Id);

                // 4. Confirmar transacción
                await transaction.CommitAsync();
                return evento.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarEstadoEvento(int id, int estadoId)
        {
            var evento = await context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
            if (evento == null) return false;

            evento.TipoEstadoId = estadoId;
            await context.SaveChangesAsync();
            return true;
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
        public async Task<bool> ActualizarEvento(int id, EventoDTO dto)
        {
            var evento = await context.Eventos.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
                return false;

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
        public async Task<bool> ActualizarRelacionesEvento(int id, EventoDTO dto)
        {
            var evento = await context.Eventos
                .Include(e => e.PacienteEventos)
                .Include(e => e.EventoUsuarios)
                .Include(e => e.EventoLugarHechos)
                .Include(e => e.EventoMovils)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null) return false;

            evento.PacienteEventos.Clear();
            if (dto.PacienteIds != null && dto.PacienteIds.Any())
            {
                foreach (var pid in dto.PacienteIds)
                    evento.PacienteEventos.Add(new PacienteEvento { PacienteId = pid, EventoId = id });
            }

            evento.EventoUsuarios.Clear();
            if (dto.UsuarioIds != null && dto.UsuarioIds.Any())
            {
                foreach (var uid in dto.UsuarioIds)
                    evento.EventoUsuarios.Add(new EventoUsuario { UsuarioId = uid, EventoId = id });
            }

            evento.EventoLugarHechos.Clear();
            if (dto.LugarHechoIds != null && dto.LugarHechoIds.Any())
            {
                foreach (var lid in dto.LugarHechoIds)
                    evento.EventoLugarHechos.Add(new EventoLugarHecho { LugarHechoId = lid, EventoId = id });
            }

            evento.EventoMovils.Clear();
            if (dto.MovilIds != null && dto.MovilIds.Any())
            {
                foreach (var mid in dto.MovilIds)
                    evento.EventoMovils.Add(new EventoMovil { MovilId = mid, EventoId = id });
            }
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActualizarEventoPaciente(int id, EventoCrearDTO dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                // 1. Buscar Evento existente
                var evento = await context.Eventos
                    .Include(e => e.PacienteEventos)
                    .Include(e => e.EventoUsuarios)
                    .Include(e => e.EventoLugarHechos)
                    .Include(e => e.EventoMovils)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (evento == null)
                    return false;

                // 2. Actualizar datos básicos del evento
                evento.Relato = dto.Relato;
                evento.Ubicacion = dto.Ubicacion;
                evento.Telefono = dto.Telefono;
                evento.FechaHora = dto.FechaHora;
                evento.colorEvento = dto.colorEvento;
                evento.CausaId = dto.CausaId;
                evento.TipoEstadoId = dto.TipoEstadoId;

                await context.SaveChangesAsync();

                // 3. Actualizar pacientes asociados
                evento.PacienteEventos.Clear();

                // Pacientes existentes (por Id)
                if (dto.PacienteIds != null && dto.PacienteIds.Any())
                {
                    foreach (var pid in dto.PacienteIds)
                    {
                        evento.PacienteEventos.Add(new PacienteEvento
                        {
                            PacienteId = pid,
                            EventoId = evento.Id,
                            DiagnosticoPresuntivo = "Sin Diagnóstico"
                        });
                    }
                }

                // Pacientes nuevos (DTO completo)
                if (dto.Pacientes != null && dto.Pacientes.Any())
                {
                    foreach (var pacienteDto in dto.Pacientes)
                    {
                        Sexo sexoConvertido = Enum.Parse<Sexo>(pacienteDto.Persona.Sexo);

                        var persona = new Persona
                        {
                            Nombre = pacienteDto.Persona.Nombre,
                            DNI = pacienteDto.Persona.DNI,
                            Legajo = pacienteDto.Persona.Legajo,
                            Direccion = pacienteDto.Persona.Direccion,
                            Sexo = sexoConvertido,
                            Edad = pacienteDto.Persona.Edad
                        };
                        await context.Persona.AddAsync(persona);
                        await context.SaveChangesAsync();

                        var paciente = new Paciente
                        {
                            HistoriaClinica = await GenerarCodigoUnico(),
                            ObraSocial = pacienteDto.ObraSocial,
                            PersonaId = persona.Id
                        };
                        await context.Pacientes.AddAsync(paciente);
                        await context.SaveChangesAsync();

                        evento.PacienteEventos.Add(new PacienteEvento
                        {
                            PacienteId = paciente.Id,
                            EventoId = evento.Id,
                            DiagnosticoPresuntivo = "Sin Diagnóstico"
                        });
                    }
                }

                // 4. Actualizar otras relaciones N:M
                evento.EventoUsuarios.Clear();
                if (dto.UsuarioIds != null && dto.UsuarioIds.Any())
                {
                    foreach (var uid in dto.UsuarioIds)
                        evento.EventoUsuarios.Add(new EventoUsuario { UsuarioId = uid, EventoId = evento.Id });
                }

                evento.EventoLugarHechos.Clear();
                if (dto.LugarHechoIds != null && dto.LugarHechoIds.Any())
                {
                    foreach (var lid in dto.LugarHechoIds)
                        evento.EventoLugarHechos.Add(new EventoLugarHecho { LugarHechoId = lid, EventoId = evento.Id });
                }

                evento.EventoMovils.Clear();
                if (dto.MovilIds != null && dto.MovilIds.Any())
                {
                    foreach (var mid in dto.MovilIds)
                        evento.EventoMovils.Add(new EventoMovil { MovilId = mid, EventoId = evento.Id });
                }

                await context.SaveChangesAsync();

                // 5. Confirmar transacción
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            var evento = await context.Eventos
                .Include(e => e.PacienteEventos)
                .Include(e => e.EventoUsuarios)
                .Include(e => e.EventoLugarHechos)
                .Include(e => e.EventoMovils)
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
