using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace G1Emergency2025.Server.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio repositorio;
        public UsuarioController(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioListadoDTO>>> GetList()
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
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var Usuario = await repositorio.SelectById(id);
            if (Usuario is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(Usuario);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(UsuarioDTO DTO)
        {
            try
            {
                Usuario entidad = new Usuario
                {
                    Nombre = DTO.Nombre,
                    Contrasena = DTO.Contrasena,
                    PersonaId = DTO.PersonaId
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
        public async Task<ActionResult> Put(int id, UsuarioDTO DTO)
        {
            var entidad = new Usuario
            {
                Id = id,
                Nombre = DTO.Nombre,
                Contrasena = DTO.Contrasena,
                PersonaId = DTO.PersonaId
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
