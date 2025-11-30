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

        [HttpGet("ListaTripulantesEnMoviles")]
        public async Task<ActionResult<List<TripulanteEnMovilDTO>>> GetListaTripulantesEnMoviles()
        {
            var lista = await repositorio.SelectTripulanteEnMoviles();
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

        [HttpGet("tripulacionPorEvento/{eventoCod}")]
        public async Task<IActionResult> ObtenerTripulacion(string eventoCod)
        {
            var tripulacion = await repositorio.ObtenerTripulantesPorEvento(eventoCod);

            if (tripulacion == null || !tripulacion.Any())
                return NotFound(new { message = "No se encontró tripulación para este evento." });

            return Ok(tripulacion);
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

        [HttpPost("asignarTripulanteAlMovil")]
        public async Task<IActionResult> AsignarTripulante([FromBody] AsignarTripulanteActualDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await repositorio.AsignarTripulante(dto.TripulanteId, dto.MovilId);

                return Ok(new
                {
                    message = "Tripulante asignado correctamente",
                    tripulanteId = dto.TripulanteId,
                    movilId = dto.MovilId,
                    fechaEntrada = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Error al asignar tripulante", detail = ex.Message });
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

        [HttpPost("finalizarEvento/{eventoId}")]
        public async Task<IActionResult> FinalizarEvento(int eventoId)
        {
            try
            {
                await repositorio.FinalizarEvento(eventoId);
                return Ok(new { message = $"Evento {eventoId} finalizado correctamente. Tripulantes liberados." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al finalizar el evento: {ex.Message}" });
            }
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
