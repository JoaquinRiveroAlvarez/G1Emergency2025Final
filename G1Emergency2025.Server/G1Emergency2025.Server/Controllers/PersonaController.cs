using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.IRepositorios;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Persona")]

    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepositorio repositorio;
        public PersonaController(IPersonaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaListadoDTO>>> GetList()
        {
            var lista = await repositorio.Select();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }

            return Ok(lista);
        }

        [HttpGet("Id/{id:int}")]
        public async Task<ActionResult<PersonaDTO>> GetById(int id)
        {
            var persona = await repositorio.SelectById(id);
            if (persona is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(persona);
        }

        [HttpGet("DNI/{dni}")]
        public async Task<ActionResult<PersonaDTO>> GetByDni(string dni)
        {
            var persona = await repositorio.SelectByDni(dni);
            if (persona is null)
            {
                return NotFound($"No existe el registro con el código: {dni}.");
            }

            return Ok(persona);
        }

        [HttpGet("ListaPersona")]
        public async Task<ActionResult<List<PersonaListadoDTO>>> GetListaPersona()
        {
            var lista = await repositorio.SelectListaPersona();
            if (lista == null)
            {
                return NotFound("No se encontro la lista, VERIFICAR.");
            }
            if (lista.Count == 0)
            {
                return Ok("No existen items en la lista en este momento");
            }
            return Ok(lista);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(PersonaDTO DTO)
        {
            try
            {
                Persona entidad = new Persona
                {
                    Nombre = DTO.Nombre,
                    DNI = DTO.DNI,
                    Sexo = DTO.Sexo,
                    Edad = DTO.Edad,
                    Direccion = DTO.Direccion,
                    Legajo = DTO.Legajo

                };
                var id = await repositorio.Insert(entidad);
                return Ok(entidad.Id);
            }
            catch (Exception e)
            {
                return BadRequest($"Error al crear el nuevo registro: {e.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PersonaDTO DTO)
        {
            var entidad = new Persona
            {
                Id = id,
                Nombre = DTO.Nombre,
                DNI = DTO.DNI,
                Sexo = DTO.Sexo,
                Edad = DTO.Edad,
                Direccion = DTO.Direccion,
                Legajo = DTO.Legajo
            };

            var resultado = await repositorio.Update(id, entidad);

            if (!resultado)
            {
                return BadRequest("Datos no válidos");
            }

            return Ok($"El registro con el id: {id} fue actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var resultado = await repositorio.Delete(id);
            if (!resultado)
            {
                return BadRequest("Datos no validos");
            }
            return Ok($"El registro con el id: {id} fue eliminado correctamente.");
        }
    }
}
