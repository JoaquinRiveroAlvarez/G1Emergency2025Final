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
    [Route("api/TripulacionActual")]
    public class TripulacionActualController : ControllerBase
    {
        private readonly ITripulacionActualRepositorio repositorio;
        public TripulacionActualController(ITripulacionActualRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<TripulacionActualListadoDTO>>> GetList()
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
        public async Task<ActionResult<CausaDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }
        [HttpGet("FechaEntrada/{FechaEntrada:datetime}")]
        public async Task<ActionResult<TripulacionActualDTO>> GetByFechaEntrada(DateTime FechaEntrada)
        {
            var tipoProvincia = await repositorio.SelectByFechaEntrada(FechaEntrada);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con la fecha de entrada: {FechaEntrada}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("ListaTripulacionActual")]
        public async Task<ActionResult<List<TripulacionActualListadoDTO>>> GetListaTripulacionActual()
        {
            var lista = await repositorio.SelectListaTripulacionActual();
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
        public async Task<ActionResult<int>> Post(TripulacionActualDTO DTO)
        {
            try
            {
                TripulacionActual entidad = new TripulacionActual
                {
                    FechaEntrada = DTO.FechaEntrada,
                    TripulanteId = DTO.TripulanteId,
                    MovilId = DTO.MovilId
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
        public async Task<ActionResult> Put(int id, TripulacionActualDTO DTO)
        {
            var entidad = new TripulacionActual
            {
                Id = id,
                FechaEntrada = DTO.FechaEntrada,
                TripulanteId = DTO.TripulanteId,
                MovilId = DTO.MovilId
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
