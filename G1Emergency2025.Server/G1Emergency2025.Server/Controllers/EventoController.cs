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
    [Route("api/evento")]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepositorio repositorio;
        public EventoController(IEventoRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
        [HttpGet]
        public async Task<ActionResult<List<EventoDTO>>> GetList()
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
        public async Task<ActionResult<EventoListadoDTO>> GetById(int id)
        {
            var tipoProvincia = await repositorio.SelectById(id);
            if (tipoProvincia is null)
            {
                return NotFound($"No existe el registro con el id: {id}.");
            }

            return Ok(tipoProvincia);
        }

        [HttpGet("ListaEvento")]
        public async Task<IActionResult> GetListaEvento()
        {
            var lista = await repositorio.SelectListaEvento();
            if (lista == null || !lista.Any())
                return NotFound("No hay eventos registrados.");

            return Ok(lista);
        }

        [HttpGet("ListaEventoReciente")]
        public async Task<IActionResult> GetListaEventoReciente()
        {
            var eventos = await repositorio.SelectListaEventoReciente();
            return Ok(eventos);
        }

        [HttpGet("ListaEventoConMovil")]
        public async Task<IActionResult> GetListaEventoConMovil()
        {
            var lista = await repositorio.SelectListaEventoConDisponibilidad();
            if (lista == null || !lista.Any())
                return NotFound("No hay eventos registrados.");

            return Ok(lista);
        }

        [HttpGet("EventoPorCodigo/{cod}")]
        public async Task<ActionResult<List<EventoListadoDTO>>> GetEventoCodigo(string cod)
        {
            var lista = await repositorio.SelectPorCod(cod);
            if (lista == null)
            {
                return NotFound($"No se encontro elementos en la lista con el código: {cod}.");
            }
            return Ok(lista);
        }

        [HttpGet("buscarPorFecha")]
        public async Task<ActionResult<List<EventoListadoDTO>>> BuscarPorFecha(
        [FromQuery] int? anio,
        [FromQuery] int? mes,
        [FromQuery] int? dia,
        [FromQuery] int? hora)
        {
            try
            {
                var eventos = await repositorio.SelectPorFechaFlexible(anio, mes, dia, hora);

                if (!eventos.Any())
                    return NotFound("No se encontraron eventos con esos filtros.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar eventos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEvento([FromBody] EventoDTO dto)
        {
            try
            {
                int id = await repositorio.InsertarEvento(dto);
                return Ok(id);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, EventoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("El Id del evento no coincide con el DTO enviado.");

            var actualizado = await repositorio.ActualizarEvento(id, dto);

            if (!actualizado)
                return NotFound($"No se encontró el evento con Id {id}");

            return Ok($"El evento con Id {id} fue actualizado correctamente.");
        }

        [HttpPut("{id}/estadoEvento")]
        public async Task<IActionResult> ActualizarEstadoEvento(int id, [FromBody] ActualizarEstadoEventoDTO dto)
        {
            try
            {
                var actualizado = await repositorio.ActualizarEstadoEvento(id, dto.TipoEstadoId);

                if (!actualizado)
                    return NotFound(new { message = $"No se encontró el evento con ID {id}" });

                return Ok(new { estadoActualizado = dto.TipoEstadoId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await repositorio.DeleteEvento(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(int id, EventoDTO dto)
        //{
        //    if (id != dto.Id)
        //    {
        //        return BadRequest("El Id del evento no coincide con el DTO enviado.");
        //    }

        //    var entidad = await repositorio.SelectById(id);

        //    if (entidad == null)
        //    {
        //        return NotFound($"No se encontró el evento con Id {id}");
        //    }

        //    entidad.Codigo = dto.Codigo;
        //    entidad.Ubicacion = dto.Ubicacion;
        //    entidad.Telefono = dto.Telefono;
        //    entidad.FechaHora = dto.FechaHora;
        //    entidad.colorEvento = dto.colorEvento;
        //    entidad.CausaId = dto.CausaId;
        //    entidad.TipoEstadoId = dto.TipoEstadoId;

        //    var resultado = await repositorio.Update(id, entidad);
        //    if (!resultado)
        //    {
        //        return BadRequest("Error al actualizar los datos del evento.");
        //    }

        //    var relacionesOk = await repositorio.ActualizarRelacionesEvento(id, dto);

        //    if (!relacionesOk)
        //    {
        //        return BadRequest("Error al actualizar las relaciones del evento.");
        //    }

        //    return Ok($"El evento con Id {id} fue actualizado correctamente.");
        //}
    }
}
