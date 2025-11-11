using G1Emergency2025.Shared.Enum;
using G1Emergency2025.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G1Emergency2025.BD.Datos
{
    public class AppDbContext : DbContext
    {
        public DbSet<Causa> Causas { get; set; }
        public DbSet<DiagPresuntivo> DiagPresuntivos { get; set; }
        public DbSet<Historico> Historicos { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<TipoMovil> TipoMovils { get; set; }
        public DbSet<TipoTripulante> TipoTripulantes { get; set; }
        public DbSet<TripulacionActual> TripulacionActuals { get; set; }
        public DbSet<Tripulante> Tripulantes { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<LugarHecho> LugarHechos { get; set; }
        public DbSet<Movil> Movils { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoEstado> TipoEstados { get; set; }
        public DbSet<TipoMovil> TipoMoviles { get; set; }


        public DbSet<UsuarioRol> UsuarioRols { get; set; }
        public DbSet<PacienteEvento> PacienteEventos { get; set; }
        public DbSet<EventoLugarHecho> EventoLugarHechos { get; set; }
        public DbSet<EventoUsuario> EventoUsuarios { get; set; }



        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PacienteEvento>()
                .HasKey(pp => new { pp.PacienteId, pp.EventoId });

            modelBuilder.Entity<EventoLugarHecho>()
                .HasKey(elh => new { elh.LugarHechoId, elh.EventoId });

            modelBuilder.Entity<EventoUsuario>()
                .HasKey(eu => new { eu.EventoId, eu.UsuarioId });

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.UsuarioId, ur.RolId });


            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }
        }
    }
}
