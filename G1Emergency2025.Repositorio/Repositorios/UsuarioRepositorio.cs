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
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly AppDbContext context;

        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Usuario?> SelectByCod(string cod)
        {
            return await context.Set<Usuario>().FirstOrDefaultAsync(x => x.Contrasena == cod);
        }
        public async Task<List<UsuarioListadoDTO>> SelectListaUsuario()
        {
            var lista = await context.Usuarios
                                    .Select(p => new UsuarioListadoDTO
                                    {
                                        Id = p.Id,
                                        Usuario = $" Nombre de Usuario: {p.Nombre} - Contraseña: {p.Contrasena} - Id Persona: {p.PersonaId} - Nombre y Apellido: {p.Persona!.Nombre}"
                                    })
                                    .ToListAsync();
            return lista;
        }
        public async Task AsociarEvento(int usuarioId, int eventoId)
        {
            var rel = new EventoUsuario
            {
                UsuarioId = usuarioId,
                EventoId = eventoId
            };

            context.EventoUsuarios.Add(rel);
            await context.SaveChangesAsync();
        }

    }
}
