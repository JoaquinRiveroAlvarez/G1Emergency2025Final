using G1Emergency2025.BD.Datos;
using G1Emergency2025.BD.Datos.Entity;
using G1Emergency2025.Repositorio.Repositorios;
using G1Emergency2025.Shared.DTO;
using G1Emergency2025.Shared.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2025.Server.Controllers
{
    [ApiController]
    [Route("api/Tripulante")]
    public class TripulanteController : ControllerBase
    {
        private readonly ITripulanteRepositorio repositorio;
        public TripulanteController(ITripulanteRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TripulanteListadoDTO>>> GetList()
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
        public async Task<ActionResult<TripulanteDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("EnMovil/{EnMovil:bool}")]
        public async Task<ActionResult<TripulanteDTO>> GetByEnMovil(bool EnMovil)
        {
            var tipoProvincia = await repositorio.SelectByEnMovil(EnMovil);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {EnMovil}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("ListaTripulante")]
        public async Task<ActionResult<List<TripulanteListadoDTO>>> GetListaCausa()
        {
            var lista = await repositorio.SelectListaTripulante();
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
        public async Task<ActionResult<int>> Post(TripulanteDTO DTO)
        {
            try
            {
                Tripulante entidad = new Tripulante
                {
                    EnMovil = DTO.EnMovil,
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
        public async Task<ActionResult> Put(int id, TripulanteDTO DTO)
        {
            var entidad = new Tripulante
            {
                Id = id,
                EnMovil = DTO.EnMovil,
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
