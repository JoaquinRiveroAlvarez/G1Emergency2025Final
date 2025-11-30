using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.IRepositorios;
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
    public class PersonaRepositorio : Repositorio<Persona>, IPersonaRepositorio
    {
        private readonly AppDbContext context;

        public PersonaRepositorio(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<Persona?> SelectByDni(string dni)
        {
            return await context.Set<Persona>().FirstOrDefaultAsync(x => x.DNI == dni);
        }
        public async Task<List<PersonaListadoDTO>> SelectListaPersona()
        {
            var lista = await context.Persona
            .Select(p => new PersonaListadoDTO
            {
                Id = p.Id,
                Persona = $"Nombre y Apellido: {p.Nombre} - DNI: {p.DNI} - Dirección: {p.Direccion} - Sexo: {p.Sexo} - Edad: {p.Edad}"
            })
            .ToListAsync();
            return lista;
        }
        
    }
}

